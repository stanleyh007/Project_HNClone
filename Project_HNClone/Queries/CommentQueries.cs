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

        
    }
}
