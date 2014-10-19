using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsService.Models;
using CarsService.Repositories;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Ploeh.AutoFixture;
using Xunit.Extensions;

namespace CarsService.Test.Repositories
{
    public class CarsJsonRepositoryTest : IUseFixture<CarsJsonRepository>
    {
        [Theory, AutoData]
        public void should_be_able_to_add_new_cars_to_repo(Car toBeAdded)
        {
            var repository = new CarsJsonRepository();

            repository.Add(toBeAdded);
        }

        [Theory, AutoData]
        public void should_be_able_to_persist_cars_in_repo(Car toBeAdded)
        {
            var repository = new CarsJsonRepository();
            repository.Add(toBeAdded);
            repository.Save();

            var secondRepo = new CarsJsonRepository();
            secondRepo.Load();

            Assert.Equal(1, secondRepo.GetAll().Count());
            Assert.Equal(toBeAdded, secondRepo.GetAll().Single());
        }

        [Theory, AutoData]
        public void should_be_able_to_clear_itself(Car toBeAdded)
        {
            var repository = new CarsJsonRepository();

            repository.Add(toBeAdded);
            repository.Clear();

            Assert.Equal(0, repository.GetAll().Count());
        }

        [Fact]
        public void should_be_able_to_get_all_cars_for_specified_person()
        {
            var repository = new CarsJsonRepository();
            var firstPersonCar = new Car(1, "GTC43423", "BMW", "M3");
            var secondPersonCar = new Car(2, "GD 22222", "Lamborghini", "Murcielago");
            repository.Add(firstPersonCar);
            repository.Add(secondPersonCar);

            var results = repository.GetForPersonWithId(2);
            
            Assert.Equal(
                results.Single(), 
                secondPersonCar);
        }

        public void SetFixture(CarsJsonRepository data)
        {
            data.Clear();
            data.Save();
        }
    }
}
