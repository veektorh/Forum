using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Web.Models;
using HomeViewModels;
using ProfileViewModels;

namespace ForumViewModels.ProfileViewModels
{
    
    public class ProfileDetailsViewModel
    {
        public ApplicationUser User { get; set; }        
        public IEnumerable<Community> CreatedCommunities {get;set;}
        public IEnumerable<Comment> Comments {get;set;}
        public IEnumerable<ProfilePostDetailsViewModel> Posts {get;set;}
    }
}