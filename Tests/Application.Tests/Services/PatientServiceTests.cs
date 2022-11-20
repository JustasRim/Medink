using Domain.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Application.Tests.Services
{
    public class PatientServiceTests
    {

        private ApplicationDbContext context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options);

        private Patient patient = new Patient()
        {
            Id = 1,
            Name = "Name",
            LastName = "Surname",
            Email = "test@email.com"
        };

        [Fact]
        public async void ShouldCreateNewPatient()
        {
            PatientService patientService = new PatientService(context);

            await patientService.Create(patient);
            Assert.True(context.Patients.Contains(patient));
        }

        [Fact]
        public async void ShouldDeletePatient()
        {
            PatientService patientService = new PatientService(context);

            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            await patientService.Delete(1);
            Assert.True(!context.Patients.Contains(patient));
        }

        [Fact]
        public async void ShouldGetPatient()
        {
            PatientService patientService = new PatientService(context);

            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            var gottenPatient = patientService.Get(1).Result;
            Assert.True(patient.Equals(gottenPatient));
        }

        [Fact]
        public async void ShouldUpdatePatient()
        {
            PatientService patientService = new PatientService(context);

            context.Patients.Add(patient);
            await context.SaveChangesAsync();
            string number = "37063218822";
            patient.Number = number;
            await patientService.Update(patient);
            var updatedPatient = context.Patients.Where(x => x.Id == 1).FirstOrDefault();
            Assert.True(updatedPatient.Number.Equals(number));
        }
    }
}
