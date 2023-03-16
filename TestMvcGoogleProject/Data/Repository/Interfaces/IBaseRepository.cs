using TestMvcGoogleProject.Data.Entity;

namespace TestMvcGoogleProject.Data.Repository.Interfaces;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
{
    Task Add(TEntity obj);

    Task<TEntity?> GetById(int? id);

    Task<IEnumerable<TEntity>> GetAll();

    Task Update(TEntity obj);

    Task Remove(TEntity obj);
}
