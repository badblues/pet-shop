using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using Persistence.Repositories;
using Persistence.Models;
using System.Reflection;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Persistence.Mappings;

string connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=rpbd;";

var config = Fluently.Configure()
    .Database(PostgreSQLConfiguration.PostgreSQL82
        .ConnectionString(connectionString)
        .ShowSql()) // Show SQL in the console (for debugging)
    .Mappings(m =>
    {
        m.FluentMappings
            .Add<ClientMap>(); // Add your ClientMap mapping here
    })
    .BuildConfiguration();

var sessionFactory = config.BuildSessionFactory();

using (var session = sessionFactory.OpenSession())
{
    Console.WriteLine("Session opened!");
    foreach (var mapping in config.ClassMappings)
    {
        Console.WriteLine($"Mapped Entity: {mapping.MappedClass.Name}");
    }
    ClientRepository clientRepository = new ClientRepository(session);
    Client client = new Client() { Name = "Lesha", Address = "Novosibirsk"};
    clientRepository.Add(client);
    IEnumerable<Client> clients = clientRepository.GetAll();
    foreach(Client cl in clients)
    {
        Console.WriteLine(cl.Name);
    }
}