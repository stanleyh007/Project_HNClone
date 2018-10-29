using System;
using System.Collections.Generic;

namespace Project_HNClone.Models
{
    public partial class Stories
    {
        public Stories()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string PostType { get; set; }
        public string PostUrl { get; set; }
        public int? PositiveRating { get; set; }
        public int? NegativeRating { get; set; }
        public DateTime PublishDate { get; set; }
        public int? HanesstId { get; set; }

        public Users Creator { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
