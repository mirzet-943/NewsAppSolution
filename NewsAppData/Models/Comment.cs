using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAppData
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string CommentText { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
