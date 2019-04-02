using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.Web.Models;

namespace Forum.Web.Services
{

    public interface IPostService
    {
        IQueryable<Post> GetAll();
        IQueryable<Post> GetAll(Expression<Func<Post,bool>> predicate);
        Post GetById(int id);
        void Add(Post Post);
        void Remove(Post Post);
    }
}