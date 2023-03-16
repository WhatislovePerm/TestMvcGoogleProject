using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestMvcGoogleProject.Models;
using Microsoft.AspNetCore.Authorization;

using TestMvcGoogleProject.Services.Interfaces;

namespace TestMvcGoogleProject.Controllers;

public class HomeController : Controller
{
    private readonly IPostService _postService;

    public HomeController( IPostService postService) =>
        _postService = postService;
    
    public async Task<IActionResult> Index()
    {
        if (User.Identity != null && !User.Identity.IsAuthenticated)
            return View(new List<PostViewModel>());
        
        var userId = User.Claims.Where(x => x.Issuer == "LOCAL AUTHORITY").Select(x => x.Value).First();
        var posts = await _postService.GetAllPosts(userId);
        return View(posts);
    }

    [Authorize]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();
        
        var post = await _postService.GetPost(id.Value);

        return View(post);
    }
    
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("Title,Text")] CreatePostViewModel createPost)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.Where(x => x.Issuer == "LOCAL AUTHORITY").Select(x => x.Value).First();
            await  _postService.CreatePost(createPost, userId);

            return RedirectToAction(nameof(Index));
        }
        return View(createPost);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var post = await _postService.GetPost(id.Value);

        return View(post);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Text,Date,UserEmail")] PostViewModel postViewModel)
    {
        if (id != postViewModel.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            var userId = User.Claims.Where(x => x.Issuer == "LOCAL AUTHORITY").Select(x => x.Value).First();

            await _postService.EditPost(postViewModel, userId);

            return RedirectToAction(nameof(Index));
        }

        return View(postViewModel);
    }

    [Authorize]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var post = await _postService.GetPost(id.Value);

        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _postService.DeletePost(id);
        return RedirectToAction(nameof(Index));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

