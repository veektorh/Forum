using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostViewModels;
using Microsoft.AspNetCore.Identity;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly IPostService _postService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICommentService _commentService;
        public PostController(ICommunityService communityService, IPostService postService, UserManager<ApplicationUser> userManager, ICommentService commentService)
        {
            _communityService = communityService;
            _postService = postService;
            _userManager = userManager;
            _commentService = commentService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new PostCreateViewModel();
            model.Communities = GetCommunityDropdown();
            try
            {
                return View(model);
            }
            catch(Exception ex)
            {
                return View(model);
            }
        }

        

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel post)
        {
            
            post.Communities = GetCommunityDropdown();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
            
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var newPost = new Post(){
                    Title = post.Title,
                    Body = post.Body,
                    CommunityId = int.Parse(post.CommunityId),
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Id,                
                };

                 _postService.Add(newPost);

                return RedirectToAction("Index","Home");
            }
            catch(Exception ex)
            {
                return View(post);
            }
        }
    
        public IActionResult Details(int id)
        {
            var post = _postService.GetById(id);
            var postModel = Helper.ConvertToHomePostIndexViewModel(post,_commentService);
            var comments = _commentService.GetAll(item => item.PostId == id).ToList();

            var model = new PostDetailsViewModel(){
                Post = postModel,
                Comments = Helper.ConvertToCommentIndexViewModel(comments)
            };

            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> AddComment(int id, string comment)
        {
            try{
                if(String.IsNullOrEmpty(comment))
                {
                    return RedirectToAction("Details",new {id=id});
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var commentModel = new Comment(){
                    Body = comment,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Id,
                    PostId = id,
                    ParentCommentId = null,
                };
                _commentService.Add(commentModel);

                return RedirectToAction("Details",new {id=id});

            }
            catch(Exception ex)
            {
                return RedirectToAction("Details",new {id=id});
            }
        }
        
        public IEnumerable<SelectListItem> GetCommunityDropdown()
        {
            return _communityService.GetAll().ToList().Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                });
        }
    }
}