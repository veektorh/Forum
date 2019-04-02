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
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Comment> GetAll(){
            return _context.Comments.Include(a=>a.ParentComment).Include(b=>b.Post).Include(c=>c.User);
        }

        public IQueryable<Comment> GetAll(Expression<Func<Comment,bool>> predicate){
            return _context.Comments.Include(a=>a.ParentComment).Include(b=>b.Post).Include(c=>c.User).Where(predicate);
            
        }

        public Comment GetById(int id){
            return _context.Comments.Include(a=>a.ParentComment).Include(b=>b.Post).Include(c=>c.User).FirstOrDefault(a=>a.Id==id);
        }

        public void Add(Comment Comment){
            _context.Comments.Add(Comment);
            _context.SaveChanges();
        }

        public void Remove(Comment Comment){
            _context.Comments.Remove(Comment);
            _context.SaveChanges();
        }
    }
}
