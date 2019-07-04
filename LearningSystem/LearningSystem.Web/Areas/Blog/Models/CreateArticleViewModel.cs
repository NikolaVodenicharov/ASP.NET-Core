using LearningSystem.Data.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Blog.Models
{
    public class CreateArticleViewModel
    {
        [Required]
        [MinLength(ArticleConstants.TitleMinLenght)]
        [MaxLength(ArticleConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public List<SelectListItem> Authors { get; set; }
    }
}
