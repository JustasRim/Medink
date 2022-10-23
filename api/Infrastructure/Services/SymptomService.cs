using api.Application.Common.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    internal class SymptomService : ISymptomService
    {
        private readonly IApplicationDbContext _context;

        public SymptomService(IApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<int> Create(Symptom symptom)
        {
            try
            {
                _context.Symptoms.Add(symptom);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task<int> Delete(int id)
        {
            _context.Symptoms.Remove(new Symptom { Id = id });
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<Symptom>> Get()
        {
            return await _context.Symptoms.ToListAsync();
        }

        public async Task<Symptom?> Get(int id)
        {
            return await _context.Symptoms.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Symptom?> Update(Symptom symptom)
        {
            var symptomToUpdate = await _context.Symptoms.FirstOrDefaultAsync(q => q.Id == symptom.Id);

            if (symptomToUpdate is null)
            {
                return null;
            }

            symptomToUpdate.Description = symptom.Description;
            symptomToUpdate.Name = symptom.Name;
            symptomToUpdate.PatientId = symptom.PatientId;

            await _context.SaveChangesAsync();
            return symptomToUpdate;
        }
    }
}
