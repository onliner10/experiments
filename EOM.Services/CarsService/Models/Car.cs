using System;

namespace CarsService.Models
{
    public struct Car
    {
        public Car(int personId, string registrationNumber, string brand, string model) : this()
        {
            PersonId = personId;
            RegistrationNumber = registrationNumber;
            Brand = brand;
            Model = model;
        }

        public int PersonId { get; set; }
        public string RegistrationNumber { get; set; }
        public String Brand { get; set; }
        public string Model { get; set; }
    }
}