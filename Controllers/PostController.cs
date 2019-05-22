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
using CommunityViewModels;
using Extensions;

namespace Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly IPostService _postService;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ICommentService _commentService;

        private readonly ICommentStateService _CommentStateService;

        private readonly IPostStateService _PostStateService;
        public PostController(ICommunityService communityService, IPostService postService, UserManager<ApplicationUser> userManager, ICommentService commentService, ICommentStateService CommentStateService, IPostStateService PostStateService)
        {
            _communityService = communityService;
            _postService = postService;
            _userManager = userManager;
            _commentService = commentService;
            _CommentStateService = CommentStateService;
            _PostStateService = PostStateService;
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
            catch (Exception ex)
            {
                var msg = ex.Message;
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
                var newPost = new Post()
                {
                    Title = post.Title,
                    Body = post.Body,
                    CommunityId = int.Parse(post.CommunityId),
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Id,
                };

                _postService.Add(newPost);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return View(post);
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var post = _postService.GetById(id);
                var postModel = post.ConvertToHomePostIndexViewModel(_commentService); 
                var comments = _commentService.GetAll(item => item.PostId == id).ToList();

                var model = new PostDetailsViewModel()
                {
                    Post = postModel,
                    Comments = comments.ConvertToCommentIndexViewModel()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult GetCommentsJson(int id)
        {
            try
            {
                var comment = _commentService.GetAll(item => item.PostId == id).OrderByDescending(a => a.CreatedAt).ToList();
                var commentModel = comment.ConvertToCommentIndexViewModel();
                return Json(new { comments = commentModel });

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(new { comments = new CommentIndexViewModel() });
            }
        }

        [Authorize]
        public async Task<IActionResult> AddComment(int id, string comment)
        {
            try
            {

                if (String.IsNullOrEmpty(comment))
                {
                    return Json(false);
                }
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var commentModel = new Comment()
                {
                    Body = comment,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Id,
                    PostId = id,
                    ParentCommentId = null,
                };
                _commentService.Add(commentModel);

                return Json(true);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(false);
            }
        }

        [Authorize]
        public async Task<IActionResult> UpVotePost(int id) {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var res = _PostStateService.UpvotePost(user.Id,id);
                return Ok(new {status = true, count = res} );
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return Ok(new {status = false, count = 0} );
            }
         }

        [Authorize]
        public async Task<IActionResult> DownVotePost(int id) {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var res = _PostStateService.DownvotePost(user.Id,id);
                return Ok(new {status = true, count = res} );
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return Ok(new {status = false, count = 0} );
            }
         }

        [Authorize]
        public async Task<IActionResult> UpVoteComment(int id) {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var res = _CommentStateService.UpvoteComment(user.Id,id);
                return Ok(new {status = true, count = res} );
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return Ok(new {status = false, count = 0});
            }
         }
        [Authorize]
        public async Task<IActionResult> DownVoteComment(int id) {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var res = _CommentStateService.DownvoteComment(user.Id,id);
                return Ok(new {status = true, count = res} );
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return Ok(new {status = false, count = 0});
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