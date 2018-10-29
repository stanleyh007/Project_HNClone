using System;
using System.Collections.Generic;

namespace Project_HNClone.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; set; }
        public int StoryId { get; set; }
        public DateTime PublishDate { get; set; }
        public int? HanesstId { get; set; }

        public Users Owner { get; set; }
        public Stories Story { get; set; }
    }
}
