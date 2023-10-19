using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

string connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=rpbd;";

var config = new Configuration();

config.DataBaseIntegration(d =>
{
    d.ConnectionString = connectionString;
    d.Dialect<PostgreSQLDialect>();
    d.Driver<NpgsqlDriver>();
});

config.AddAssembly(Assembly.GetExecutingAssembly());
var sessionFactory = config.BuildSessionFactory();

using (var session = sessionFactory.OpenSession())
{
    Console.WriteLine("Session opened!");

}