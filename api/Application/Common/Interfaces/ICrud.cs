using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ICrud<T>
{
    Task<int> Create(T obj);

    Task<T?> Update(T obj);

    Task<IList<T>> Get();

    Task<T?> Get(int id);

    T? Get(Func<T, bool> action);

    Task<int> Delete(int id);
}