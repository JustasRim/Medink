using api.Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

internal class PatientService : IPatientService
{
    private readonly IApplicationDbContext _context;

    public PatientService(IApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<int> Create(Patient patient)
    {
        _context.Patients.Add(patient);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        _context.Patients.Remove(new Patient { Id = id });
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

    public async Task<int> Update(Patient patient)
    {
        var patientToUpdate = await _context.Patients.FirstOrDefaultAsync(q => q.Id == patient.Id);

        if (patientToUpdate is null)
        {
            return -1;
        }

        patientToUpdate.Email = patient.Email;
        patientToUpdate.Name = patient.Name;
        patientToUpdate.LastName = patient.LastName;
        patientToUpdate.Number = patient.Number;
        patientToUpdate.MedicId = patient.MedicId;

        return await _context.SaveChangesAsync();
    }
}
