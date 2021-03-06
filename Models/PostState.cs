
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Web.Models
{
    
    public class PostState
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string UserId { get; set; }

        public bool Vote {get;set;}

        public bool Saved {get;set;}

        [ForeignKey("PostId")]
        public virtual Post Post {get;set;}

        [ForeignKey("UserId")]
        public virtual ApplicationUser User {get;set;}
        
    }
}