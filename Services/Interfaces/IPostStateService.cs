using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.Web.Models;

namespace Forum.Web.Services
{

    public interface IPostStateService
    {
        IQueryable<PostState> GetAll();
        IQueryable<PostState> GetAll(Expression<Func<PostState,bool>> predicate);
        PostState GetById(int id);
        void Add(PostState Post);
        void Remove(PostState Post);

        int UpvotePost(string userId , int postId);

        int DownvotePost(string userId , int postId);
    }
}