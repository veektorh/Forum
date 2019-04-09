using System.Linq;
using System.Collections.Generic;
using Forum.Web.Models;
using Forum.Web.Services;
using HomeViewModels;
using Services;


namespace Extensions
{
    public static class PostModelExtensions
    {
        public static HomePostIndexViewModel ConvertToHomePostIndexViewModel(this Post post,ICommentService _commentService)
        {
            if(post == null || _commentService == null)
            {
                return new HomePostIndexViewModel();
            }

            return new HomePostIndexViewModel(){
                Body = post.Body,
                Community = post.Community,
                CommunityId = post.CommunityId,
                CreatedAt = post.CreatedAt,
                CreatedBy = post.CreatedBy,
                Id = post.Id,
                RelatedTime = Helper.ConvertToRelativeDateTime(post.CreatedAt),
                Title = post.Title,
                Upvotes = post.Upvotes,
                User = post.User,
                commentCount = _commentService.GetAll(comment =>comment.PostId == post.Id).Count()
            };
        }

        public static List<HomePostIndexViewModel> ConvertToHomePostIndexViewModel(this List<Post> post, ICommentService _commentService)
        {
            if(post == null || _commentService == null)
            {
                return new List<HomePostIndexViewModel>();
            }
            return post.Select(item => new HomePostIndexViewModel(){
                Body = item.Body,
                Community = item.Community,
                CommunityId = item.CommunityId,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                Id = item.Id,
                RelatedTime = Helper.ConvertToRelativeDateTime(item.CreatedAt),
                Title = item.Title,
                Upvotes = item.Upvotes,
                User = item.User,
                commentCount = _commentService.GetAll(comment =>comment.PostId == item.Id).Count()
            }).ToList();
        }

    }
}