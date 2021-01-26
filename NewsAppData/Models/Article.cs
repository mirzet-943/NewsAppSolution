using System;
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
        public string WriterFullName { get; set; }
        public int WriterId { get; set; }
        public int Likes { get; set; }
    }
}
