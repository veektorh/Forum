
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Web.Models
{
    
    public class Community
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedBy {get;set;}

        public DateTime CreatedAt {get;set;}

        [ForeignKey("CreatedBy")]
        public ApplicationUser User {get;set;}
    }
}