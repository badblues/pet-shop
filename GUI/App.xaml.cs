using System.Windows;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using GUI.Views;
using GUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Repositories;
using Persistence.Mappings;
using NHibernate;
using System;
using GUI.Core;
using GUI.Services;
using System.Diagnostics;

namespace GUI;

public partial class App : Application
{
    private readonly IHost _host;
    private readonly ISession _session;

    private string _connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=rpbd;";

    public App()
    {
        var config = Fluently.Configure()
            .Database(PostgreSQLConfiguration.PostgreSQL82
                .ConnectionString(_connectionString))
            .Mappings(m =>
            {
                m.FluentMappings
                    .Add<ClientMapping>()
                    .Add<BreedMapping>();
            }).
            ExposeConfiguration(cfg =>
            {
                // Create a custom SQL output listener that sends SQL statements to the debug output window.
                cfg.SetProperty(NHibernate.Cfg.Environment.ShowSql, "true");
                cfg.SetProperty(NHibernate.Cfg.Environment.FormatSql, "true");
                cfg.SetProperty(NHibernate.Cfg.Environment.UseSqlComments, "true");
                cfg.SetInterceptor(new DebugSqlStatementInterceptor());
            })
            .BuildConfiguration();

        var sessionFactory = config.BuildSessionFactory();
        _session = sessionFactory.OpenSession();

        _host = Host.CreateDefaultBuilder()
        .ConfigureServices((services) =>
        {
            services.AddScoped(provider => new ClientRepository(_session));
            services.AddScoped(provider => new BreedRepository(_session));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<ClientsViewModel>();
            services.AddSingleton<BreedsViewModel>();
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
        _host.StopAsync();
        _host.Dispose();
        _session.Close();

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