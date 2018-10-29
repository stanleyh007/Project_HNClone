using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HNCloneApi.data
{
    public class StoryAndComment
    {
        public string post_title { get; set; }
        public string post_text { get; set; }
        public int hanesst_id { get; set; }
        public string post_type { get; set; }
        public int post_parent { get; set; }
        public string username { get; set; }
        public string pwd_hash { get; set; }
        public string post_url { get; set; }
    }
}
