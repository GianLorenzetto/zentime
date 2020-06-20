using System.Collections.Generic;
using CommandLine;
using Serilog;

namespace ZenTime.Database
{
    public class Options
    {
        [Option('c', "connection", Required = true, HelpText = "Database connection string.")]
        public string ConnectionString { get; set; }

        [Option("drop-database", Required = false, HelpText = "Drop database before running migrations.")]
        public bool DropDatabase { get; set; }

        [Option("seed", Required = false, HelpText = "Post migrations, seed data will be inserted into the database")]
        public bool InsertSeedData { get; set; }
    }

    static class Program
    {
        private static int _returnCode = 0;
            
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
            
            return _returnCode;
        }

        static void RunOptions(Options opts)
        {
            var migrator = new DatabaseMigrator(opts.ConnectionString);
            
            if (opts.DropDatabase)
            {
                Log.Warning("!!! Dropping database !!!");
                migrator.DropDatabase();
            }

            Log.Information("*** Ensuring database exists ***");
            migrator.EnsureDatabaseExists();

            Log.Information("^^^ Running incremental migrations ^^^");
            var result = migrator.RunIncrementalMigrations();

            Log.Information("Database migration completed: {Result}", result);
            if (!result.Successful)
            {
                Log.Error("Database migration failed: {ErrorMessage}", result.Error);
                _returnCode = 1;
                return;
            }

            if (opts.InsertSeedData)
            {
                Log.Information("+++ Inserting seed data +++");
                result = migrator.SeedDatabase();

                Log.Information("Insert seed data completed: {Result}", result);
                if (!result.Successful)
                {
                    Log.Error("Insert seed data failed: {ErrorMessage}", result.Error);
                    _returnCode = 1;
                }
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
           Log.Error("Error parsing command line arguments: {Errors}", errs);
        }
    }
}