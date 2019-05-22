using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.Services;
using Microsoft.AspNetCore.Identity;
using ForumViewModels.CommunityViewModels;
using CommunityViewModels;
using System.Linq;
using Services;
using Microsoft.AspNetCore.Authorization;
using Extensions;

namespace Forum.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunityService _communityService;

        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        
        public CommunityController(ICommunityService communityService, IPostService postService,UserManager<ApplicationUser> userManager, ICommentService commentService)
        {
            _userManager = userManager;
            _communityService = communityService;
            _postService = postService;
            _commentService = commentService;
        }
        public IActionResult Index(){
            var community = _communityService.GetAll().ToList();
            
            
            return View(community);
        }

        public IActionResult Details(int id){
            try
            {
                var community = _communityService.GetById(id);
                var posts = _postService.GetAll(item => item.CommunityId == id).OrderByDescending(a => a.CreatedAt).ToList();

                var model = new CommunityDetailsViewModel();
                model.Posts = posts.ConvertToHomePostIndexViewModel(_commentService);
                model.Community = community;

                return View(model);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Error","Home");
            }
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CommunityCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var name = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var community = new Community();
                community.CreatedAt = DateTime.Now;
                community.CreatedBy = user.Id;
                community.Description = model.Description;
                community.Name = model.Name;
                
                _communityService.Add(community);
                return RedirectToAction("index");
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("index");
            }
        }
    
        
    }
}