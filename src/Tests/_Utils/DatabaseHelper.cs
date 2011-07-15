using System;
using System.Data.SqlServerCe;
using System.IO;

namespace ProcentCqrs.Tests._Utils
{
    public class DatabaseHelper
    {
        private static readonly string _dbPath = Path.Combine(PathUtil.GetTestFilesLocation(), "db.sdf");
        public static readonly string ConnectionString = "Data Source = {0}; Password = p".FormatWith(_dbPath);

        private static readonly string _scriptsPath = "../../../ProcentCqrsDb/Schema Objects";

        public static void CreateDb()
        {
            Directory.CreateDirectory(PathUtil.GetTestFilesLocation());

            File.Delete(_dbPath);

            using (var engine = new SqlCeEngine(ConnectionString))
            {
                engine.CreateDatabase();
            }

            ExecuteInitScripts();
        }

        private static void ExecuteInitScripts()
        {
            string[] scriptFiles = Directory.GetFiles(_scriptsPath, "*.sql", SearchOption.AllDirectories);

            var cePreparer = new SqlServerCeScriptProcessor();

            using (SqlCeConnection connection = new SqlCeConnection(ConnectionString))
            {
                connection.Open();
                foreach (var scriptFile in cePreparer.PrepareScripts(scriptFiles))
                {
                    using (var command = new SqlCeCommand(scriptFile.PreparedContent, connection))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception exc)
                        {
                            throw new Exception("File {0} failed".FormatWith(Path.GetFileName(scriptFile.Path)), exc);
                        }
                    }
                }
            }
        }
    }
}