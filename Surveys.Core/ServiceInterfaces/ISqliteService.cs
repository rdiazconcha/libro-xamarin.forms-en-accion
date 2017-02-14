using SQLite;

namespace Surveys.Core.ServiceInterfaces
{
    public interface ISqliteService
    {
        SQLiteConnection GetConnection();
    }
}