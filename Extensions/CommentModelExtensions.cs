using System.Collections.Generic;
using System.Linq;
using Forum.Web.Models;
using Forum.Web.Services;
using HomeViewModels;
using Services;
using CommunityViewModels;


namespace Extensions
{
    public static class CommentModelExtensions
    {
        
        public static List<CommentIndexViewModel> ConvertToCommentIndexViewModel(this List<Comment> comment)
        {
            if(comment == null )
            {
                return new List<CommentIndexViewModel>();
            }

            return comment.Select(item => new CommentIndexViewModel(){
                Body = item.Body,
                ParentCommentId = item.ParentCommentId,
                PostId = item.PostId,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                Id = item.Id,
                Relatedtime = Helper.ConvertToRelativeDateTime(item.CreatedAt),
                Post = item.Post,
                Upvotes = item.Upvotes,
                User = item.User,
                 
            }).ToList();
        }

        
    }
}