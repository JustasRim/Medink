using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Medic> Medics { get; }
    
    DbSet<Patient> Patients { get; }

    DbSet<Symptom> Symptoms { get; }

    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync();
}

