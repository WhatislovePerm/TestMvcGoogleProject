using TestMvcGoogleProject.Data.Entity;

namespace TestMvcGoogleProject.Data.Repository.Interfaces;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<List<Post>> GetAllPosts(string userid);
}