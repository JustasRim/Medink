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

    public async Task<int> CreateMedic(Medic medic)
    {
        _context.Medics.Add(medic);

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        _context.Medics.Remove(new Medic { Id = id });
        return await _context.SaveChangesAsync();
    }

    public async Task<Medic?> GetMedic(int id)
    {
        return await _context.Medics.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IList<Medic>> GetMedics()
    {
        return await _context.Medics.ToListAsync();
    }

    public async Task<int> UpdateMedic(Medic medic)
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
}