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
using ForumViewModels;
using ForumViewModels.ProfileViewModels;
using Extensions;

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

        public async Task<IActionResult> Details(string username){    
            var user = await _userManager.FindByNameAsync(username);
            var posts = _postService.GetAll().Where(p => p.CreatedBy == user.Id).OrderByDescending(a => a.CreatedAt).Take(20).ToList();
            var vm = new ProfileDetailsViewModel()
            {
                User = user,
                CreatedCommunities = _communityService.GetAll().Where(p => p.CreatedBy == user.Id).ToList(),
                Comments = _commentService.GetAll().Where(p => p.CreatedBy == user.Id).ToList(),
                Posts = posts.ConvertToProfilePostDetailsViewModel(_commentService)
            };

            return View(vm);
        }
    }
}