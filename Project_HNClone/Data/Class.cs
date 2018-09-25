using System.Data.SQLite;

namespace Project_HNClone.Data
{
    public class Database
    {
        static void Main(string[] args)
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "create table users (name nvarchar, password nvarchar, karma int, publishDate datetime, primary key (name))";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "create table stories (name nvarchar, content nvarchar, creatorID int, positiveRating int, negativeRating int, publishDate datetime, foreign key(creatorID) references users(ROWID))";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "create table comments (content nvarchar, ownerID int, storyID int, publishDate datetime, foreign key(ownerID) references users(ROWID), foreign key(storyID) references stories(ROWID))";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

    }
}
