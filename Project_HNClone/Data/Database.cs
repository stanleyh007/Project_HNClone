using System.Data.SQLite;

namespace Project_HNClone.Data
{
    public class Database
    {
        public void Startup()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "create table users (name nvarchar, password nvarchar, karma int, publishDate datetime, primary key (name))";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "create table stories (name nvarchar, content nvarchar, creatorID int, creatorName nvarchar, postType nvarchar, postURL nvarchar, positiveRating int, negativeRating int, publishDate datetime, foreign key(creatorID) references users(ROWID))";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "create table comments (content nvarchar, ownerID int, storyID int, publishDate datetime, foreign key(ownerID) references users(ROWID), foreign key(storyID) references stories(ROWID))";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into users values (system, admin, 9000, 2018-01-01 00:00:00)";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void addUser(User user)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "insert into users values (' + user.name + ', ' + user.password + ', ' + ' 0, ' , admin, 9000, 2018-01-01 00:00:00)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }

        public void addComment(Comment comment)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "insert into users values (system, admin, 9000, 2018-01-01 00:00:00)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public void addStory(Story story)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "insert into users values (system, admin, 9000, 2018-01-01 00:00:00)";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


    }
}
