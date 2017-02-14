using SQLite;
using Surveys.Core.ServiceInterfaces;
using Surveys.iOS.Services;

[assembly:Xamarin.Forms.Dependency(typeof(SqliteService))]

namespace Surveys.iOS.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile =
                System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    "surveys.db");
            return new SQLiteConnection(localDbFile);
        }
    }
}