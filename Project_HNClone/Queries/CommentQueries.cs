using Microsoft.Extensions.Configuration;
using Project_HNClone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Project_HNClone.Queries
{
    public class CommentQueries
    {
        private readonly IConfiguration configuration;

        public CommentQueries(IConfiguration config)
        {
            this.configuration = config;
        }

        public bool CreateComment(String content, int ownerID, int storyID)
        {
            bool verified = true;
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = "INSERT INTO Comments VALUES ('@CONTENT', '@OWNERID', '@STORYID');";
                    command.Parameters.Add("@CONTENT", SqlDbType.NVarChar).Value = content;
                    command.Parameters.Add("@OWNERID", SqlDbType.Int).Value = ownerID;
                    command.Parameters.Add("@STORYID", SqlDbType.Int).Value = storyID;


                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        verified = false;
                    }



                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
            return verified;
        }
    }
}
