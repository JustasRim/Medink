using Domain.Entities;

namespace Application.Common.Interfaces;

public interface ICrud<T>
{
    Task<int> Create(T obj);

    Task<int> Update(T obj);

    Task<IList<T>> Get();

    Task<T?> Get(int id);

    Task<int> Delete(int id);
}