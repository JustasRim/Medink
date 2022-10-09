using api.Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MedicService : IMedicService
{
    private readonly IApplicationDbContext _context;

    public MedicService(IApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<int> Create(Medic medic)
    {
        _context.Medics.Add(medic);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        _context.Medics.Remove(new Medic { Id = id });
        return await _context.SaveChangesAsync();
    }

    public async Task<Medic?> Get(int id)
    {
        return await _context.Medics.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IList<Medic>> Get()
    {
        return await _context.Medics.ToListAsync();
    }

    public async Task<int> Update(Medic medic)
    {
        var medicToUpdate = await _context.Medics.FirstOrDefaultAsync(q => q.Id == medic.Id);

        if (medicToUpdate is null)
        {
            return -1;
        }

        medicToUpdate.Email = medic.Email;
        medicToUpdate.Name = medic.Name;
        medicToUpdate.LastName = medic.LastName;
        medicToUpdate.Number = medic.Number;

        return await _context.SaveChangesAsync();
    }

    public IList<Symptom>? GetSymptoms(int medicId, int patientId)
    {
        var symptoms = _context.Medics
            .Include(q => q.Patients)!
            .ThenInclude(q => q.Symptoms)
            .FirstOrDefault(q => q.Id == medicId)
            ?.Patients?.FirstOrDefault(q => q.Id == patientId)
            ?.Symptoms;
        
        if (symptoms == null)
        {
            return new List<Symptom>();
        }

        foreach (var symptom in symptoms)
        {
            symptom.Patient = null;
        }

        return symptoms.ToList();
    }
}