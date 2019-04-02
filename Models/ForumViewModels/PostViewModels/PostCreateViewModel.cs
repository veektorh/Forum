
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PostViewModels{
    public class PostCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Body { get; set; }

        [Required]
        public string CommunityId { get; set; }

        public IEnumerable<SelectListItem> Communities {get;set;}
    }
}