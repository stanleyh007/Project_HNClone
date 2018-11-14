using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HNCloneApi.data
{
    public class LogMessages
    {
        public IPAddress IpAddress { get; set; }
        public string Username { get; set; }
        public DateTime DateTime { get; set; }
        public int HttpStatusCode { get; set; }
        public string Message { get; set; }
    }
}
