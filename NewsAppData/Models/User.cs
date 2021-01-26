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

        [Required]
        public int Role { get; set; }
        [Required,MinLength(5)]
        public string Username { get; set; }
        [Required,MinLength(6)]
        public string Password { get; set; }
        [Required, MinLength(4)]
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}