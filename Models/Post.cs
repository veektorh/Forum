
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Web.Models
{
    
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}

        public int Upvotes {get;set;}

        public int CommunityId {get;set;}

        [ForeignKey("CommunityId")]
        public virtual Community Community {get;set;}
        
        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser User {get;set;}

        
    }
}