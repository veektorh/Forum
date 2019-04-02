
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Web.Models;
using HomeViewModels;

namespace CommunityViewModels
{
    
    public class CommunityDetailsViewModel
    {
        public Community Community {get;set;}
        public List<HomePostIndexViewModel> Posts {get;set;}
    }
}