using System;
using System.ComponentModel.DataAnnotations;

namespace Project_HNClone.Models
{
  public class LoginViewModel
  {
    public string Username { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
