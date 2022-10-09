using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Services;

public interface IMedicService : ICrud<Medic>
{
    IList<Symptom>? GetSymptoms(int medicId, int patientId);
}
