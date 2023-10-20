using System.Windows;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using GUI.View;
using GUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Repositories;
using Persistence.Mappings;
using NHibernate;

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
            services.AddSingleton<MainWindow>();
            services.AddScoped(provider => new ClientRepository(_session));
            services.AddSingleton<MainVM>();
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

        base.OnExit(e);
    }
}