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
    public class PostStateService : IPostStateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;
        public PostStateService(ApplicationDbContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        public IQueryable<PostState> GetAll(){
            return _context.PostStates.Include(b=>b.Post);
        }

        public IQueryable<PostState> GetAll(Expression<Func<PostState,bool>> predicate){
            return _context.PostStates.Include(b=>b.Post).Where(predicate);
            
        }

        public int UpvotePost(string userId , int postId)
        {
            var _userAlreadyUpvoted = UserAlreadyUpvoted(userId,postId);
            if(_userAlreadyUpvoted == null)
            {
                var model = new PostState(){
                    PostId = postId,
                    UserId = userId,
                    Vote = true
                };
                Add(model);
                return _postService.IncrementUpvote(postId);
                
            }

            var upvote = _userAlreadyUpvoted.Vote;
            if(upvote){

                return _userAlreadyUpvoted.Post.Upvotes;
            }

            _userAlreadyUpvoted.Vote = true;
            _context.SaveChanges();
            return _postService.IncrementUpvote(postId);
            
        }

        public int DownvotePost(string userId , int postId)
        {
            var _userAlreadyUpvoted = UserAlreadyUpvoted(userId,postId);
            if(_userAlreadyUpvoted == null)
            {
                var model = new PostState(){
                    PostId = postId,
                    UserId = userId,
                    Vote = false
                };
                Add(model);
                return _postService.DecrementUpvote(postId);
                
            }

            var upvote = _userAlreadyUpvoted.Vote;
            if(!upvote){
                return _userAlreadyUpvoted.Post.Upvotes;
            }

            _userAlreadyUpvoted.Vote = false;
            _context.SaveChanges();
            return _postService.DecrementUpvote(postId);
            
        }

        private PostState UserAlreadyUpvoted(string userId , int postId)
        {
            return GetAll(a => a.UserId == userId && a.PostId == postId).FirstOrDefault();
        }

        public PostState GetById(int id){
            return _context.PostStates.Include(b=>b.Post).Where(a => a.Id == id).FirstOrDefault(a=>a.Id==id);
        }

        public void Add(PostState PostState){
            _context.PostStates.Add(PostState);
            _context.SaveChanges();
        }

        public void Remove(PostState PostState){
            _context.PostStates.Remove(PostState);
            _context.SaveChanges();
        }

       
    }
}
