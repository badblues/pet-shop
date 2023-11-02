namespace Persistence.Repositories;

internal interface IRepository<T>
{
    T Get(int id);
    ICollection<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);

}
