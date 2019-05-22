
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Web.Models
{
    
    public class Comment
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public string CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}

        public int Upvotes {get;set;}

        public int? ParentCommentId {get;set;}

        [ForeignKey("ParentCommentId")]
        public virtual Comment ParentComment {get;set;}
        public int PostId {get;set;}

        [ForeignKey("PostId")]
        public virtual Post Post {get;set;}
        
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser User {get;set;}

        
    }
}