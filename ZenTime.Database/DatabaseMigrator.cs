using System.Reflection;
using DbUp;
using DbUp.Builder;
using DbUp.Engine;
using DbUp.Helpers;

namespace ZenTime.Database
{
    public class DatabaseMigrator
    {
        private readonly string _connectionString;

        public DatabaseMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void DropDatabase()
        {
            DbUp.DropDatabase.For.SqlDatabase(_connectionString);
        }

        public void EnsureDatabaseExists()
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);
        }

        public DatabaseUpgradeResult RunIncrementalMigrations()
        {
            var upgrader = CreateUpgradeBuilder("IncrementalScripts")
                .JournalToSqlTable("dbo", "_SchemaVersions")
                .Build();

            return upgrader.PerformUpgrade();
        }
        
        public DatabaseUpgradeResult SeedDatabase()
        {
            // var options = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<ZenTimeDbContext>(), _connectionString).Options;
            // var context = new ZenTimeDbContext(options);
            // await ZenTimeDbLocalDev.InsertSeedData(context, new DateTimeOffsetProvider());
            var upgrader = CreateUpgradeBuilder("SeedDataScripts")
                .JournalTo(new NullJournal())
                .Build();
            
            return upgrader.PerformUpgrade();
        }
        private UpgradeEngineBuilder CreateUpgradeBuilder(string filter)
        {
            return DeployChanges.To
                .SqlDatabase(_connectionString)
                .WithoutTransaction()
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                    name => name.Contains(filter))
                .LogToAutodetectedLog();
        }
    }
}