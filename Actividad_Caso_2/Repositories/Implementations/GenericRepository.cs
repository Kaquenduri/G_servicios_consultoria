using Actividad_Caso_2.Data;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    protected readonly GestionProyectosContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(GestionProyectosContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(params object[] keyValues)
    {
        return await _dbSet.FindAsync(keyValues);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}