using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.Web.Models;

namespace Forum.Web.Services
{

    public interface ICommentStateService
    {
        IQueryable<CommentState> GetAll();
        IQueryable<CommentState> GetAll(Expression<Func<CommentState,bool>> predicate);
        CommentState GetById(int id);
        void Add(CommentState Comment);
        void Remove(CommentState Comment);

        int UpvoteComment(string userId , int CommentId);

        int DownvoteComment(string userId , int CommentId);
    }
}