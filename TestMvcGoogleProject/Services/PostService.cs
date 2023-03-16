using TestMvcGoogleProject.Data.Entity;
using TestMvcGoogleProject.Data.Repository.Interfaces;
using TestMvcGoogleProject.Models;
using TestMvcGoogleProject.Services.Interfaces;

namespace TestMvcGoogleProject.Services;

public class PostService : IPostService
{
    public PostService(IPostRepository postRepository, IAccountRepository accountRepository)
    {
        _postRepository = postRepository;
        _accountRepository = accountRepository;
    }

    private readonly IPostRepository _postRepository;

    private readonly IAccountRepository _accountRepository;
    
    public async Task CreatePost(CreatePostViewModel createPostViewModel, string userid)
    {
        var post = new Post(createPostViewModel);
        var user = await _accountRepository.GetById(userid);
        
        post.User = user;
        post.Date = DateTime.Now;
        
        await _postRepository.Add(post);
    }

    public async Task<PostViewModel> GetPost(int id)
    {
        var post = await _postRepository.GetById(id);

        if (post == null)
            return new PostViewModel();
        
        return new PostViewModel()
        {
            Id = id,
            Title = post.Title,
            Text = post.Text,
            Date = post.Date,
            UserEmail = post.User.Email
        };
    }

    public async Task EditPost(PostViewModel postViewModel, string userId)
    {
        var post = new Post(postViewModel)
        {
            Date = DateTime.Now,
            UserId = userId
        };

        await _postRepository.Update(post);
    }

    public async Task DeletePost(int id)
    {
        var post = new Post
        {
            Id = id
        };

        await _postRepository.Remove(post);
    }

    public async Task<List<PostViewModel>> GetAllPosts(string userid)
    {
        var posts = await _postRepository.GetAllPosts(userid);
        
        return posts
            .Select(x =>
                new PostViewModel()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Date = x.Date,
                    Title = x.Title,
                    UserEmail = x.User.Email  
                }).ToList();
    }
}