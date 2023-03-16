using TestMvcGoogleProject.Data.DataContext;
using TestMvcGoogleProject.Data.Entity;
using TestMvcGoogleProject.Data.Repository.Interfaces;

namespace TestMvcGoogleProject.Data.Repository;

public class AccountRepository : BaseRepository<ApplicationUser>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {}

    public async Task<ApplicationUser?> GetById(string id)
    {
         return await db.Set<ApplicationUser>().FindAsync(id);
    }
}