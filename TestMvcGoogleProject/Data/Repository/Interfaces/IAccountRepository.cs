using TestMvcGoogleProject.Data.Entity;

namespace TestMvcGoogleProject.Data.Repository.Interfaces;

public interface IAccountRepository : IBaseRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetById(string id);
}