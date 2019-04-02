
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Forum.Web.Models;

namespace CommunityViewModels
{
    
    public class CommunityindexViewModel
    {
        public List<Community> communityList {get;set;}
    }
}