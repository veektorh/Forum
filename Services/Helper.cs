

using System;
using System.Collections.Generic;
using System.Linq;
using Forum.Web.Models;
using HomeViewModels;
using Forum.Web.Services;
using CommunityViewModels;

namespace Services{
    public class Helper{

         public static List<CommentIndexViewModel> ConvertToCommentIndexViewModel(List<Comment> comment)
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
        public static List<HomePostIndexViewModel> ConvertToHomePostIndexViewModel(List<Post> post, ICommentService _commentService)
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

        public static HomePostIndexViewModel ConvertToHomePostIndexViewModel(Post post, ICommentService _commentService)
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
        public static string ConvertToRelativeDateTime(DateTime date)
        {
            var sec = Convert.ToInt32((DateTime.Now - date).TotalSeconds);
            var min = Convert.ToInt32((DateTime.Now - date).TotalMinutes);
            var hour = Convert.ToInt32((DateTime.Now - date).TotalHours);
            var day = Convert.ToInt32((DateTime.Now - date).TotalDays);
            
            if(min < 1)
            {
                return sec + " secs ago";
            }

            if (hour < 1)
            {
                return min + " mins ago";
            }

            if (hour < 25)
            {
                return hour + " hours ago";
            }

            return day + " days ago";

        }
    }
}