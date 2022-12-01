using api.Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly IApplicationDbContext _context;

    public PatientService(IApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<int> Create(Patient patient)
    {
        try
        {
            _context.Patients.Add(patient);
            return await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public async Task<int> Delete(int id)
    {
        var patient = _context.Patients.Where(p => p.Id == id).FirstOrDefault();
        _context.Patients.Remove(patient);
        return await _context.SaveChangesAsync();
    }

    public async Task<IList<Patient>> Get()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> Get(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(q => q.Id == id);
    }

    public Patient? Get(Func<Patient, bool> predicate)
    {
        return _context.Patients.FirstOrDefault(predicate);
    }

    public async Task<Patient?> Update(Patient patient)
    {
        var patientToUpdate = await _context.Patients.FirstOrDefaultAsync(q => q.Id == patient.Id);

        if (patientToUpdate is null)
        {
            return null;
        }

        patientToUpdate.Email = patient.Email;
        patientToUpdate.Name = patient.Name;
        patientToUpdate.LastName = patient.LastName;
        patientToUpdate.Number = patient.Number;
        patientToUpdate.MedicId = patient.MedicId;

        await _context.SaveChangesAsync();
        return patientToUpdate;
    }
}
