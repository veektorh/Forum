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
    public class CommentStateService : ICommentStateService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentService _CommentService;
        public CommentStateService(ApplicationDbContext context, ICommentService CommentService)
        {
            _context = context;
            _CommentService = CommentService;
        }

        public IQueryable<CommentState> GetAll(){
            return _context.CommentStates.Include(b=>b.Comment);
        }

        public IQueryable<CommentState> GetAll(Expression<Func<CommentState,bool>> predicate){
            return _context.CommentStates.Include(b=>b.Comment).Where(predicate);
            
        }

        public int UpvoteComment(string userId , int CommentId)
        {
            var _userAlreadyUpvoted = UserAlreadyUpvoted(userId,CommentId);
            if(_userAlreadyUpvoted == null)
            {
                var model = new CommentState(){
                    CommentId = CommentId,
                    UserId = userId,
                    Vote = true
                };
                Add(model);
                return _CommentService.IncrementUpvote(CommentId);
                
            }

            var upvote = _userAlreadyUpvoted.Vote;
            if(upvote){

                return _userAlreadyUpvoted.Comment.Upvotes;
            }

            _userAlreadyUpvoted.Vote = true;
            _context.SaveChanges();
           return _CommentService.IncrementUpvote(CommentId);
            
        }

        public int DownvoteComment(string userId , int CommentId)
        {
            var _userAlreadyUpvoted = UserAlreadyUpvoted(userId,CommentId);
            if(_userAlreadyUpvoted == null)
            {
                var model = new CommentState(){
                    CommentId = CommentId,
                    UserId = userId,
                    Vote = false
                };
                Add(model);
                return _CommentService.DecrementUpvote(CommentId);
                
            }

            var upvote = _userAlreadyUpvoted.Vote;
            if(!upvote){
                return _userAlreadyUpvoted.Comment.Upvotes;
            }

            _userAlreadyUpvoted.Vote = false;
            _context.SaveChanges();
            return _CommentService.DecrementUpvote(CommentId);
            
        }

        private CommentState UserAlreadyUpvoted(string userId , int CommentId)
        {
            return GetAll(a => a.UserId == userId && a.CommentId == CommentId).FirstOrDefault();
        }

        public CommentState GetById(int id){
            return _context.CommentStates.Include(b=>b.Comment).Where(a => a.Id == id).FirstOrDefault(a=>a.Id==id);
        }

        public void Add(CommentState CommentState){
            _context.CommentStates.Add(CommentState);
            _context.SaveChanges();
        }

        public void Remove(CommentState CommentState){
            _context.CommentStates.Remove(CommentState);
            _context.SaveChanges();
        }
    }
}
