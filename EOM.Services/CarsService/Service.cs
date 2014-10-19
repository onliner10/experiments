using CarsService.Models;
using CarsService.Repositories;
using Nancy.ModelBinding;

namespace CarsService
{
    using Nancy;

    public class Service : NancyModule
    {
        private readonly CarsJsonRepository _repo = new CarsJsonRepository();

        public Service()
        {
            _repo.Load();

            Get["/Cars/{id}"] = parameters =>
            {
                int id = parameters.id;
                var cars = _repo.GetForPersonWithId(id);

                return View["cars.sshtml", cars];
            };

            Get["/Cars/Add"] = 
                parameters => View["add.html"];

            Post["/Cars/Add"] = parameters =>
            {
                var car = this.Bind<Car>();
                _repo.Add(car);
                _repo.Save();

                return "";
            };
        }
    }
}