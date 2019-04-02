using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Forum.Web.Data;
using Forum.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Post> GetAll(){
            return _context.Posts.Include("Community.User");
        }

        public IQueryable<Post> GetAll(Expression<Func<Post,bool>> predicate){
            return _context.Posts.Include("Community.User").Where(predicate);
        }

        public Post GetById(int id){
            return _context.Posts.Include("Community.User").FirstOrDefault(a => a.Id==id);
        }

        public void Add(Post post){
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Remove(Post post){
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
