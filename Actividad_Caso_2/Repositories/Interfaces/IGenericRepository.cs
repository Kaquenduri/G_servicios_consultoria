namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(params object[] keyValues);

    Task AddAsync(T entity);

    void Update(T entity);

    void Remove(T entity);
}