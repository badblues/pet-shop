using System;
using System.Diagnostics;
using System.Windows;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GUI.Core;
using GUI.Services;
using GUI.ViewModels;
using GUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using Persistence.Mappings;
using Persistence.Repositories;

namespace GUI;

public partial class App : Application
{
    private readonly IHost _host;
    private readonly ISession _session;

    private readonly string _connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=rpbd;";

    public App()
    {
        NHibernate.Cfg.Configuration config = Fluently.Configure()
            .Database(PostgreSQLConfiguration.PostgreSQL82
                .ConnectionString(_connectionString))
            .Mappings(m =>
            {
                _ = m.FluentMappings
                    .Add<ClientMapping>()
                    .Add<BreedMapping>()
                    .Add<EmployeeMapping>();
            }).
            ExposeConfiguration(cfg =>
            {
                // Create a custom SQL output listener that sends SQL statements to the debug output window.
                _ = cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                _ = cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                _ = cfg.SetProperty(NHibernate.Cfg.Environment.UseSqlComments, "true");
                _ = cfg.SetInterceptor(new DebugSqlStatementInterceptor());
            })
            .BuildConfiguration();

        ISessionFactory sessionFactory = config.BuildSessionFactory();
        _session = sessionFactory.OpenSession();

        _host = Host.CreateDefaultBuilder()
        .ConfigureServices((services) =>
        {
            _ = services.AddScoped(provider => new ClientRepository(_session));
            _ = services.AddScoped(provider => new BreedRepository(_session));
            _ = services.AddScoped(provider => new EmployeeRepository(_session));

            _ = services.AddSingleton<MainWindow>();
            _ = services.AddSingleton<MainViewModel>();
            _ = services.AddSingleton<HomeViewModel>();
            _ = services.AddSingleton<ClientsViewModel>();
            _ = services.AddSingleton<BreedsViewModel>();
            _ = services.AddSingleton<EmployeesViewModel>();

            _ = services.AddSingleton<INavigationService, NavigationService>();
            _ = services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        })
        .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _ = _host.StopAsync();
        _host.Dispose();
        _ = _session.Close();

        base.OnExit(e);
    }

    public class DebugSqlStatementInterceptor : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Debug.WriteLine(sql.ToString());
            return sql;
        }
    }
}