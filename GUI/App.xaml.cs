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
                .ConnectionString(_connectionString)
                .ShowSql()) // Show SQL in the console (for debugging)
            .Mappings(m =>
            {
                m.FluentMappings
                    .Add<ClientMap>();
            })
            .BuildConfiguration();

        var sessionFactory = config.BuildSessionFactory();
        _session = sessionFactory.OpenSession();

        _host = Host.CreateDefaultBuilder()
        .ConfigureServices((services) =>
        {
            services.AddScoped(provider => new ClientRepository(_session));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<ClientsViewModel>();
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
}