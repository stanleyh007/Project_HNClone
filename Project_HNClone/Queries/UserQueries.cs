using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_HNClone.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Project_HNClone.Queries
{
    public class UserQueries
    {
        private readonly IConfiguration configuration;

        public UserQueries(IConfiguration config)
        {
            this.configuration = config;
        }

        public bool VerifyUser(String name, String password)
        {
            bool verified = false;
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader;
                command.Connection = connection;

                try
                {
                    command.CommandText = "SELECT * FROM Users WHERE Users.Name = @NAME AND Users.Password = @PASSWORD";
                    command.Parameters.Add("@NAME", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@PASSWORD", SqlDbType.NVarChar).Value = password;


                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i) == "Name")
                            {
                                if ((String)reader.GetValue(i) == name)
                                {
                                    verified = true;
                                } else
                                {
                                    verified = false;
                                }
                            }
                            if (reader.GetName(i) == "Password")
                            {
                                if ((String)reader.GetValue(i) == password)
                                {
                                    verified = true;
                                }
                                else
                                {
                                    verified = false;
                                }
                            }
                        }
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
