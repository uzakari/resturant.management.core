using Application.Repositories.ResturantApp.Base.Interface;
using Domain.DB;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.ResturantApp.Base;

public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
{
    private readonly ResturantDBContext _resturantDBContext;

    public BaseRepository(ResturantDBContext resturantDBContext)
    {
        _resturantDBContext = resturantDBContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _resturantDBContext.Set<TEntity>().AddAsync(entity);
        await _resturantDBContext.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _resturantDBContext.Set<TEntity>().ToListAsync();
    }

    public async virtual Task<TEntity> GetByIdAsync(int Id)
    {
        return await _resturantDBContext.Set<TEntity>().FindAsync(Id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _resturantDBContext.SaveChangesAsync();
    }

}
