
using System;
using Forum.Web.Models;

namespace HomeViewModels{
    public class HomePostIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}

        public string RelatedTime {get;set;}

        public int commentCount {get;set;}

        public int Upvotes {get;set;}

        public int CommunityId {get;set;}

        public virtual Community Community {get;set;}
        
        public virtual ApplicationUser User {get;set;}
        
    }
}