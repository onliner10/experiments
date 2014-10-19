using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CarsService.Models;
using CarsService.Repositories;
using Nancy;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Nancy.Testing;
using Xunit.Extensions;

namespace CarsService.Test
{
    public class ServiceTests
    {
        [Fact]
        public void get_Index_displays_all_cars_for_given_person()
        {
            var firstPersonCar = new Car(1, "GTC43423", "BMW", "M3");
            var secondPersonCar = new Car(2, "GD 22222", "Lamborghini", "Murcielago");

            var repository = new CarsJsonRepository();
            repository.Add(firstPersonCar);
            repository.Add(secondPersonCar);
            repository.Save();

            var browser = GetBrowser();

            var result = browser.Get("/Cars/2",
                with => with.HttpRequest());

            var model = result.GetModel<IEnumerable<Car>>();

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("cars.sshtml", result.GetViewName());
            Assert.Equal(
                model.Single(),
                secondPersonCar);

            result
                .Body["div[class='car']"]
                .ShouldExistOnce();
        }

        [Fact]
        public void get_ADD_displays_add_form()
        {
            var browser = GetBrowser();

            var result = browser.Get("/Cars/Add",
                with =>
                {
                    with.FormValue("Id", "2");
                    with.HttpRequest();
                });

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("add.html", result.GetViewName());
        }

        [Theory, AutoData]
        public void post_ADD_adds_car(Car expected)
        {
            var browser = GetBrowser();
            var repository = new CarsJsonRepository();
            repository.Clear();
            repository.Save();

            var result = browser.Post("/Cars/Add",
                with =>
                {
                    with.HttpRequest();
                    with.FormValue("PersonId", expected.PersonId.ToString());
                    with.FormValue("RegistrationNumber", expected.RegistrationNumber);
                    with.FormValue("Model", expected.Model);
                    with.FormValue("Brand", expected.Brand);

                });

            repository.Load();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(expected, repository.GetAll().Single());
        }

        private static Browser GetBrowser()
        {
            var bootstrapper = new ConfigurableBootstrapper(
                with =>
                    with
                        .Module<Service>()
                        .RootPathProvider(new TestPathProvider())
                        .ViewFactory<TestingViewFactory>()
                );

            var browser = new Browser(bootstrapper);
            return browser;
        }
    }
}
