using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class User 
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Nick { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(User user)
        {
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            Password = user.Password;
            Nick = user.Nick;
        }
    }
}
