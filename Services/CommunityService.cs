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
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class Communityservice : ICommunityService
    {
        private readonly ApplicationDbContext _context;
        public Communityservice(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Community> GetAll(){
            return _context.Communities.Include("User");
        }

        public IQueryable<Community> GetAll(Expression<Func<Community,bool>> predicate){
            return _context.Communities.Include("User").Where(predicate);
        }

        public Community GetById(int id){
            return _context.Communities.Include("User").FirstOrDefault(item => item.Id==id);
        }

        public void Add(Community Community){
            _context.Communities.Add(Community);
            _context.SaveChanges();
        }

        public void Remove(Community Community){
            _context.Communities.Remove(Community);
            _context.SaveChanges();
        }
    }
}
