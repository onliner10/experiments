using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarsService.Models;
using Newtonsoft.Json;

namespace CarsService.Repositories
{
    public class CarsJsonRepository
    {
        private const string DataFile = "cars.json";

        private IList<Car> _cars = new List<Car>();

        public void Load()
        {
            string jsonizedData = File.ReadAllText(DataFile);
            _cars = JsonConvert.DeserializeObject<IList<Car>>(jsonizedData);
        }

        public void Save()
        {
            string serialized = JsonConvert.SerializeObject(_cars);
            File.WriteAllText(DataFile, serialized);
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public IEnumerable<Car> GetAll()
        {
            return _cars.AsEnumerable();
        }

        public IEnumerable<Car> GetForPersonWithId(int id)
        {
            return _cars.Where(c => c.PersonId == id).AsEnumerable();
        }

        public void Clear()
        {
            _cars.Clear();
        }
    }
}