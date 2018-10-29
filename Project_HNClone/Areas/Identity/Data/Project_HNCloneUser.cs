using System;
using Microsoft.AspNetCore.Identity;

namespace Project_HNClone.Areas.Identity.Data
{
  public class Project_HNCloneUser : IdentityUser
  {
    public string Name { get; set; }

    public string Password { get; set; }

    public int Karma { get; set; }

    public DateTime CreationDate { get; set; }
  }
}
