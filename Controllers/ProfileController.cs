using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Forum.Web.Models;
using Forum.Web.Models.ManageViewModels;
using Forum.Web.Services;
using ProfileViewModels;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunityService _communityService;

        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        
        public ProfileController(ICommunityService communityService, IPostService postService,UserManager<ApplicationUser> userManager, ICommentService commentService)
        {
            _userManager = userManager;
            _communityService = communityService;
            _postService = postService;
            _commentService = commentService;
        }
        public IActionResult Index(){
    
            return View();
        }

        public async Task<IActionResult> Details(string userId){    
            var vm = new ProfileDetailsViewModel() 
            {
                User = await _userManager.FindByIdAsync(userId),
                CreatedCommunities = _communityService.GetAll().Where(p => p.CreatedBy == userId),
                Comments = _commentService.GetAll().Where(p => p.CreatedBy == userId),
                Posts = _postService.GetAll().Where(p => p.CreatedBy == userId)
            };

            return View(vm);
        }
    }
}