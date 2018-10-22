using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;

namespace Project_HNClone
{
    /// <summary>
    /// Test class to find how to use connectiongstrings in core projects and it works so use this as an example
    /// </summary>
    public class test
    {
        public void testCake(string connstr)
        {
            using (SqlConnection connection = new SqlConnection(connstr))
            {
                SqlCommand command = new SqlCommand();
                SqlDataReader reader;
                command.Connection = connection;

                try
                {
                    command.CommandText = "SELECT TOP 100 * FROM dbo.Users";

                    connection.Open();
                    reader = command.ExecuteReader();

                    //while (reader.Read())
                    //{
                    //    BattleData battleData = new BattleData();
                    //    for (int i = 0; i < reader.FieldCount; i++)
                    //    {
                    //        if (reader.GetName(i) == "battleID")
                    //        {
                    //            battleData.ID = (int)reader.GetValue(i);
                    //        }
                    //        if (reader.GetName(i) == "type")
                    //        {
                    //            battleData.Type = (string)reader.GetValue(i);
                    //        }
                    //        if (reader.GetName(i) == "attacker")
                    //        {
                    //            battleData.Attacker = (string)reader.GetValue(i);
                    //        }
                    //        if (reader.GetName(i) == "defender")
                    //        {
                    //            battleData.Defender = (string)reader.GetValue(i);
                    //        }
                    //        if (reader.GetName(i) == "region")
                    //        {
                    //            battleData.Region = (string)reader.GetValue(i);
                    //        }
                    //        if (reader.GetName(i) == "date")
                    //        {
                    //            battleData.Date = (DateTime)reader.GetValue(i);
                    //        }
                    //    }
                    //    battleDatas.Add(battleData);
                    //}
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }
    }
}
