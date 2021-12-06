using System.IO;
using System.Text.Json;
using IO.Swagger.Models;

namespace IO.Swagger.Managers
{
    class DriveManager
    {
        private readonly string dbFilesExtension = "dbs";

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool SaveDatabaseOnDrive(string pathToTheDriveDirectory, Database database)
        {
            if (!Directory.Exists(pathToTheDriveDirectory))
            {
                Directory.CreateDirectory(pathToTheDriveDirectory);
            }
            var options = new JsonSerializerOptions { WriteIndented = true };

            string fileName = $"{database.GetName()}.{dbFilesExtension}";
            string jsonString = JsonSerializer.Serialize(database, options);
            File.WriteAllText($"{pathToTheDriveDirectory}{fileName}", jsonString);
            return true;
        }

        public Database? LoadDatabaseFromDrive(string path, string databaseName)
        {
            string fileName = $"{databaseName}.{dbFilesExtension}";
            if (!File.Exists($"{path}{fileName}")) return null;
            string jsonString = File.ReadAllText($"{path}{fileName}");
            
            Database databaseFromDrive = JsonSerializer.Deserialize<Database>(jsonString);
            return databaseFromDrive;
        }

        public bool RemoveDatabaseFromDrive(string path, string databaseName)
        {
            string fileName = $"{databaseName}.{dbFilesExtension}";
            if (File.Exists($"{path}{fileName}"))
            {
                File.Delete($"{path}{fileName}");
                return true;
            }

            return false;
        }
    }
}