using TestMvcGoogleProject.Models;

namespace TestMvcGoogleProject.Services.Interfaces;

public interface IPostService
{
    Task CreatePost(CreatePostViewModel createPostViewModel, string userid);
    Task EditPost(PostViewModel postViewModel, string userid);
    Task<PostViewModel> GetPost(int id);
    Task DeletePost(int id);
    Task<List<PostViewModel>> GetAllPosts(string userid);
}