using api.Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<Medic> Medics => Set<Medic>();

        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Symptom> Symptoms => Set<Symptom>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
