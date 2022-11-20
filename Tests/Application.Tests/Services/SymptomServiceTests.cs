using Domain.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Application.Tests.Services
{
    public class SymptomServiceTests
    {

        private ApplicationDbContext context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options);

        private Symptom symptom = new Symptom()
        {
            Id = 1,
            Name = "Name",
            Description = "Description",
        };

        [Fact]
        public async void ShouldCreateNewSymptom()
        {
            SymptomService symptomService = new SymptomService(context);

            await symptomService.Create(symptom);
            Assert.True(context.Symptoms.Contains(symptom));
        }

        [Fact]
        public async void ShouldDeleteSymptom()
        {
            SymptomService symptomService = new SymptomService(context);

            context.Symptoms.Add(symptom);
            await context.SaveChangesAsync();
            await symptomService.Delete(1);
            Assert.True(!context.Symptoms.Contains(symptom));
        }

        [Fact]
        public async void ShouldGetSymptom()
        {
            SymptomService symptomService = new SymptomService(context);

            context.Symptoms.Add(symptom);
            await context.SaveChangesAsync();
            var gottenSymptom = symptomService.Get(1).Result;
            Assert.True(symptom.Equals(gottenSymptom));
        }

        [Fact]
        public async void ShouldUpdateSymptom()
        {
            SymptomService symptomService = new SymptomService(context);

            context.Symptoms.Add(symptom);
            await context.SaveChangesAsync();
            string description = "New description";
            symptom.Description = description;
            await symptomService.Update(symptom);
            var updatedSymptom = context.Symptoms.Where(x => x.Id == 1).FirstOrDefault();
            Assert.True(updatedSymptom.Description.Equals(description));
        }
    }
}
