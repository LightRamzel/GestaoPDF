namespace GestaoPDF.Infra.Data.DataConfiguration
{
    public static class Constants
    {
        private static string? _path;
        public const string DatabaseFilename = "GestaoPdfDatabase.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string SetDatabasePath(string path) =>
            _path = path;

        public static string DatabasePath
        {
            get
            {
                if (string.IsNullOrEmpty(_path))
                    throw new Exception($"Caminho do arquivo {DatabaseFilename} inválido ou não encontrado");

                return Path.Combine(_path, DatabaseFilename);
            }
        }
    }
}
