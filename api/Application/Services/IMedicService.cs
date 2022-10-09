using Domain.Entities;

namespace Application.Services;

public interface IMedicService
{
    Task<int> CreateMedic(Medic medic);

    Task<bool> UpdateMedic(Medic medic);

    Task<IList<Medic>> GetMedics();

    Task<Medic?> GetMedic(int id);

    Task<int> Delete(int id);
}
