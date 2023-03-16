using Microsoft.EntityFrameworkCore;
using TestMvcGoogleProject.Data.DataContext;
using TestMvcGoogleProject.Data.Entity;
using TestMvcGoogleProject.Data.Repository.Interfaces;

namespace TestMvcGoogleProject.Data.Repository;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
         
    }

    public override Task<Post?> GetById(int? id)
    {
        return Task.FromResult(db.Posts.Include(p => p.User).FirstOrDefault(x => x.Id == id));
    }

    public async Task<List<Post>> GetAllPosts(string userid)
    {
        return await db.Posts.Include(x => x.User).Where(u => u.UserId == userid).ToListAsync();
    }
}