
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Web.Models
{
    
    public class CommentState
    {
        public int Id { get; set; }

        public int CommentId {get;set;}

        public string UserId {get;set;}

        public bool Vote {get;set;}

        public bool Saved {get;set;}

        [ForeignKey("CommentId")]
        public virtual Comment Comment {get;set;}

        [ForeignKey("UserId")]
        public virtual ApplicationUser User {get;set;}
    }
}