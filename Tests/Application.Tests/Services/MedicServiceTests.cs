using Domain.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Application.Tests.Services
{
    public class MedicServiceTests
    {

        private ApplicationDbContext context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options);

        private Medic medic = new Medic()
        {
            Id = 1,
            Name = "Name",
            LastName = "Surname",
            Email = "test@email.com",
            Patients = new List<Patient>()
        };
        private Medic medic2 = new Medic()
        {
            Id = 2,
            Name = "Name2",
            LastName = "Surname2",
            Email = "test2@email.com"
        };
        private Patient patient = new Patient()
        {
            Id = 1,
            Name = "Name",
            LastName = "Surname",
            Email = "test@email.com",
            MedicId = 1,
            Medic = new Medic(),
            Symptoms = new List<Symptom>()
        };
        private Symptom symptom1 = new Symptom()
        {
            Id = 1,
            Name = "Name",
            Description = "Description",
            Patient = new Patient(),
            PatientId = 1
        };
        private Symptom symptom2 = new Symptom()
        {
            Id = 2,
            Name = "Name2",
            Description = "Description2",
            Patient = new Patient(),
            PatientId = 1,
        };

        [Fact]
        public async void ShouldCreateNewMedic()
        {
            MedicService medicService = new MedicService(context);

            await medicService.Create(medic);
            Assert.True(context.Medics.Contains(medic));
        }

        [Fact]
        public async void ShouldDeleteMedic()
        {
            MedicService medicService = new MedicService(context);

            context.Medics.Add(medic);
            await context.SaveChangesAsync();
            await medicService.Delete(1);
            Assert.True(!context.Medics.Contains(medic));
        }

        [Fact]
        public async void ShouldGetMedic()
        {
            MedicService medicService = new MedicService(context);

            context.Medics.Add(medic);
            await context.SaveChangesAsync();
            var gottenMedic = medicService.Get(1).Result;
            Assert.True(medic.Equals(gottenMedic));
        }

        [Fact]
        public async void ShouldGetMedics()
        {
            MedicService medicService = new MedicService(context);

            context.Medics.Add(medic);
            context.Medics.Add(medic2);
            await context.SaveChangesAsync();
            var symptoms = medicService.Get().Result;
            Assert.True(symptoms.Contains(medic));
            Assert.True(symptoms.Contains(medic2));
        }

        [Fact]
        public async void ShouldUpdateMedic()
        {
            MedicService medicService = new MedicService(context);

            context.Medics.Add(medic);
            await context.SaveChangesAsync();
            string number = "37063218822";
            medic.Number = number;
            await medicService.Update(medic);
            var updatedMedic = context.Medics.Where(x => x.Id == 1).FirstOrDefault();
            Assert.True(updatedMedic.Number.Equals(number));
        }

        [Fact]
        public async void ReturnNullWhenMedicToUpdateNull()
        {
            MedicService medicService = new MedicService(context);

            var result = await medicService.Update(medic);
            Assert.True(result == null);
        }

        [Fact]
        public async void ShouldReturnEmptySymptomsList()
        {
            MedicService medicService = new MedicService(context);

            var result = medicService.GetSymptoms(1, 1);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async void ShouldGetPatientSymptoms()
        {
            MedicService medicService = new MedicService(context);
            medic.Patients.Add(patient);
            patient.Medic = medic;
            patient.Symptoms.Add(symptom1);
            patient.Symptoms.Add(symptom2);
            symptom1.Patient = patient;
            symptom2.Patient = patient;
            context.Medics.Add(medic);
            context.Patients.Add(patient);
            context.Symptoms.Add(symptom1);
            context.Symptoms.Add(symptom2);

            var symptoms = medicService.GetSymptoms(1, 1);
            Assert.True(symptoms.Contains(symptom1));
            Assert.True(symptoms.Contains(symptom2));
        }
    }
}
