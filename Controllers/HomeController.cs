using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Web.Models;
using Forum.Web.Services;
using HomeViewModels;
using Services;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly IPostService _postService;

        private readonly ICommentService _commentService;
        public HomeController(ICommunityService communityService,IPostService postService,ICommentService commentService)
        {
            _communityService = communityService;
            _postService = postService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            var posts = _postService.GetAll().OrderByDescending(a => a.CreatedAt).Take(20).ToList();

            var homeViewModel = new HomeIndexViewModel(){
                Posts = posts.ConvertToHomePostIndexViewModel(_commentService)
            };

            return View(homeViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
