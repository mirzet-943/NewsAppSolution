﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsAppData
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string Role { get; set; }
        [Required,MinLength(5)]
        public string Username { get; set; }
        [Required,MinLength(6)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required, MinLength(4)]
        public string FullName { get; set; }
        public string Token { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Username == user.Username;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
