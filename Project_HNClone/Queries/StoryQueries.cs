using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Project_HNClone.Data;

namespace Project_HNClone.Queries
{
    public class StoryQueries
    {
        private readonly string _connectionString;

        public StoryQueries(IConfiguration config)
        {
            IConfiguration configuration = config;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool CreateStory(Story story)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "INSERT INTO Stories (Name, Content, CreatorID, CreatorName, PostType, PostURL) VALUES (@Name, @Content, @CreatorID, @CreatorName, @PostType, @PostURL)";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = story.name;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = story.content;
                    command.Parameters.Add("@CreatorID", SqlDbType.Int).Value = story.creatorID;
                    command.Parameters.Add("@CreatorName", SqlDbType.NVarChar).Value = story.creatorName;
                    command.Parameters.Add("@PostType", SqlDbType.NVarChar).Value = story.postType;
                    command.Parameters.Add("@PostURL", SqlDbType.NVarChar).Value = story.postURL;
                    command.ExecuteNonQuery();

                    transaction.Commit();

                    success = true;
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                    }
                }
            }

            return success;
        }

        public Story GetStory(int id)
        {
            Story story = new Story();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = "select * from Stories where Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i) == "Id")
                            {
                                story.id = (int)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "Name")
                            {
                                story.name = (string)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "Content")
                            {
                                story.content = (string)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "CreatorID")
                            {
                                story.creatorID = (int)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "CreatorName")
                            {
                                story.creatorName = (string)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "PostType")
                            {
                                story.postType = (string)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "PostURL")
                            {
                                story.postURL = (string)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "PositiveRating")
                            {
                                story.positiveRating = (int)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "NegativeRating")
                            {
                                story.negativeRating = (int)reader.GetValue(i);
                            }
                            if (reader.GetName(i) == "PublishDate")
                            {
                                story.publishDate = (DateTime)reader.GetValue(i);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                }
            }

            return story;
        }

        public List<Story> GetStories(int amount, string postType)
        {
            List<Story> stories = new List<Story>();

            if (amount > 0)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    try
                    {
                        if (postType == "new")
                        {
                            command.CommandText = "select top @amount * from Stories order by PublishDate desc";
                            command.Parameters.AddWithValue("@amount", amount);
                        }

                        else
                        {
                            command.CommandText = "select top @amount * from Stories where PostType = @PostType order by PublishDate desc";
                            command.Parameters.AddWithValue("@amount", amount);
                            command.Parameters.AddWithValue("@PostType", postType);
                        }

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Story story = new Story();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.GetName(i) == "Id")
                                {
                                    story.id = (int)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "Name")
                                {
                                    story.name = (string)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "Content")
                                {
                                    story.content = (string)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "CreatorID")
                                {
                                    story.creatorID = (int)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "CreatorName")
                                {
                                    story.creatorName = (string)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "PostType")
                                {
                                    story.postType = (string)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "PostURL")
                                {
                                    story.postURL = (string)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "PositiveRating")
                                {
                                    story.positiveRating = (int)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "NegativeRating")
                                {
                                    story.negativeRating = (int)reader.GetValue(i);
                                }
                                if (reader.GetName(i) == "PublishDate")
                                {
                                    story.publishDate = (DateTime)reader.GetValue(i);
                                }
                            }

                            stories.Add(story);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);
                    }
                }
            }

            return stories;
        }
    }
}
