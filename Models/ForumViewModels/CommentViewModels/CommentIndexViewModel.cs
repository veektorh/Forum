using System;
using Forum.Web.Models;

namespace CommunityViewModels{
    
    public class CommentIndexViewModel
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}

        public string Relatedtime {get;set;}

        public int Upvotes {get;set;}

        public int? ParentCommentId {get;set;}

        
        public virtual Comment ParentComment {get;set;}
        public int PostId {get;set;}

        
        public virtual Post Post {get;set;}
        
        
        public virtual ApplicationUser User {get;set;}
    }
}