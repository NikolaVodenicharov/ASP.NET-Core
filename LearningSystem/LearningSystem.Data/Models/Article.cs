using LearningSystem.Data.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ArticleConstants.TitleMinLenght)]
        [MaxLength(ArticleConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public User Author { get; set; }
        public string AuthorId { get; set; }
    }
}