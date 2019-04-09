
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Web.Models;
using HomeViewModels;

namespace ProfileViewModels
{
    
    public class ProfileDetailsViewModel
    {
        public ApplicationUser User { get; set; }         
        public IEnumerable<Community> CreatedCommunities {get;set;}
        public IEnumerable<Comment> Comments {get;set;}
        public IEnumerable<Post> Posts {get;set;}
    }
}