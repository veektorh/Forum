
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomeViewModels;
using CommunityViewModels;

namespace PostViewModels{
    public class PostDetailsViewModel
    {
        
        public HomePostIndexViewModel Post { get; set; }

        public List<CommentIndexViewModel> Comments {get;set;}
        
        
    }
}