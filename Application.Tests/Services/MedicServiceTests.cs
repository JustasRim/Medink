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
            Email = "test@email.com"
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
    }
}
