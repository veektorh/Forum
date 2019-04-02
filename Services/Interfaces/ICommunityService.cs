using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Forum.Web.Models;

namespace Forum.Web.Services
{

    public interface ICommunityService
    {
        IQueryable<Community> GetAll();
        IQueryable<Community> GetAll(Expression<Func<Community,bool>> predicate);
        Community GetById(int id);
        void Add(Community Community);
        void Remove(Community Community);
    }
}