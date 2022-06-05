namespace Application.Repositories.ResturantApp.Base.Interface;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int Id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}
