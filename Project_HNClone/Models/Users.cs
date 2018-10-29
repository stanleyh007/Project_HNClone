using System;
using System.Collections.Generic;

namespace Project_HNClone.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Stories = new HashSet<Stories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Karma { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<Comments> Comments { get; set; }
        public ICollection<Stories> Stories { get; set; }
    }
}
