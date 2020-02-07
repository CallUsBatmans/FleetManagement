using System;
using System.ComponentModel.DataAnnotations;

namespace TheFleet.Services.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Username { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
        

        protected User() { }

        public User(string name, string email, string username, string password, string confirmPassword)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}