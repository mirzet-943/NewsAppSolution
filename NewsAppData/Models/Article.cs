using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAppData
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("User")]
        public int WriterId { get; set; }
        [Inheritance]
        public User Writer { get; set; }
        public int Likes { get; set; }
    }
}
