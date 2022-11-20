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
        try
        {
            _context.Medics.Add(medic);
            return await _context.SaveChangesAsync();
        } 
        catch(Exception)
        {
            return -1;
        }
    }

    public async Task<int> Delete(int id)
    {
        var medic = _context.Medics.Where(m => m.Id == id).FirstOrDefault();
        _context.Medics.Remove(medic);
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

    public async Task<Medic?> Update(Medic medic)
    {
        var medicToUpdate = await _context.Medics.FirstOrDefaultAsync(q => q.Id == medic.Id);

        if (medicToUpdate is null)
        {
            return null;
        }

        medicToUpdate.Email = medic.Email;
        medicToUpdate.Name = medic.Name;
        medicToUpdate.LastName = medic.LastName;
        medicToUpdate.Number = medic.Number;

        await _context.SaveChangesAsync();
        return medicToUpdate;
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