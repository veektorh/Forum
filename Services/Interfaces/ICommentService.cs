using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.Web.Models;

namespace Forum.Web.Services
{

    public interface ICommentService
    {
        IQueryable<Comment> GetAll();
        IQueryable<Comment> GetAll(Expression<Func<Comment,bool>> predicate);
        Comment GetById(int id);
        void Add(Comment Comment);
        void Remove(Comment Comment);

        int IncrementUpvote(int commentId);

        int DecrementUpvote(int commentId);
    }
}