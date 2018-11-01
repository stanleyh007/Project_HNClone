using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HNCloneApi.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HNCloneApi.Controllers
{
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IHttpContextAccessor _accessor;

        public StoryController(IConfiguration config, IHttpContextAccessor accessor)
        {
            IConfiguration configuration = config;
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            _accessor = accessor;
        }

        [Route("latest")]
        [HttpGet]
        public int Latest()
        {
            if (!TestIp())
            {
                return int.MinValue;
            }

            int storyLatest = 0;
            int commentLatest = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "select top 1 hanesst_id from Stories order by hanesst_id desc";

                    storyLatest = (int)(command.ExecuteScalar());

                    command.CommandText = "select top 1 hanesst_id from Comments order by hanesst_id desc";

                    commentLatest = (int)(command.ExecuteScalar());

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);
                }
            }

            int[] latest = new[] {storyLatest, commentLatest};

            return latest.Max();
        }

        [Route("status")]
        [HttpGet]
        public string Status()
        {
            return "Alive"; // give Alive, Update, or Down
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post([FromBody] StoryAndComment storyAndComment)
        {
            if (!TestIp())
            {
                return StatusCode(StatusCodes.Status403Forbidden, "403 Forbidden");
            }

            if (storyAndComment.post_parent == -1)
            {
                bool success = Story(storyAndComment);
                if (success)
                {
                    return Ok();
                }
            }
            else
            {
                bool success = Comment(storyAndComment);
                if (success)
                {
                    return Ok();
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, "400 BadRequest");
        }

        private bool Story(StoryAndComment storyAndComment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "INSERT INTO Stories (Name, Content, CreatorID, CreatorName, PostType, PostURL, hanesst_id) VALUES (@Name, @Content, @CreatorID, @CreatorName, @PostType, @PostURL, @hanesst_id)";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = storyAndComment.post_title;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = storyAndComment.post_text;
                    command.Parameters.Add("@CreatorID", SqlDbType.Int).Value = UserId(storyAndComment.username, storyAndComment.pwd_hash);
                    command.Parameters.Add("@CreatorName", SqlDbType.NVarChar).Value = storyAndComment.username;
                    command.Parameters.Add("@PostType", SqlDbType.NVarChar).Value = storyAndComment.post_type;
                    command.Parameters.Add("@PostURL", SqlDbType.NVarChar).Value = storyAndComment.post_url;
                    command.Parameters.Add("@hanesst_id", SqlDbType.Int).Value = storyAndComment.hanesst_id;
                    command.ExecuteNonQuery();

                    transaction.Commit();

                    return true;
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

                return false;
            }
        }

        private bool Comment(StoryAndComment storyAndComment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "INSERT INTO Comments (Content, OwnerID, parentHanesstId, hanesst_id) VALUES (@Content, @OwnerID, @parentHanesstId, @hanesst_id);";
                    command.Parameters.Add("@Content", SqlDbType.NVarChar).Value = storyAndComment.post_text;
                    command.Parameters.Add("@OwnerID", SqlDbType.Int).Value = UserId(storyAndComment.username, storyAndComment.pwd_hash);
                    command.Parameters.Add("@parentHanesstId", SqlDbType.Int).Value = storyAndComment.post_parent;
                    command.Parameters.Add("@hanesst_id", SqlDbType.Int).Value = storyAndComment.hanesst_id;
                    command.ExecuteNonQuery();

                    transaction.Commit();

                    return true;
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

                return false;
            }
        }

        private int UserId(string name, string password)
        {
            bool verified = false;
            int userId = -1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                try
                {
                    command.CommandText = "SELECT * FROM Users WHERE Name = @Name AND Password = @Password";
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i) == "Name")
                            {
                                if (name == (string)reader.GetValue(i))
                                {
                                    verified = true;
                                }
                                else
                                {
                                    verified = false;
                                }
                            }
                            if (reader.GetName(i) == "Password")
                            {
                                if (password == (string)reader.GetValue(i))
                                {
                                    verified = true;
                                }
                                else
                                {
                                    verified = false;
                                }
                            }
                            if (reader.GetName(i) == "Id")
                            {
                                userId = (int) reader.GetValue(i);
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

            if (verified)
            {
                return userId;
            }

            return -1;
        }

        private bool TestIp()
        {
            string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip == "46.101.225.71";
        }
    }
}
