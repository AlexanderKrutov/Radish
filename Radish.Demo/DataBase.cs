using Radish.Demo.Model;
using System.Collections.Generic;
using System.Linq;

namespace Radish.Demo
{
    public class DataBase
    {
        private List<Pet> Pets;
        private List<Person> Persons;

        private static DataBase _Instance;
        public static DataBase Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataBase();
                }
                return _Instance;
            }
        }

        private DataBase()
        {
            Persons = new List<Person>();
            Persons.Add(new Person() { Id = 1, FirstName = "Alice", LastName = "Appleseed", Age = 20, Gender = Gender.Female });
            Persons.Add(new Person() { Id = 2, FirstName = "Bob", LastName = "Brown", Age = 35, Gender = Gender.Male });
            Persons.Add(new Person() { Id = 3, FirstName = "Chris", LastName = "Campbell", Age = 27, Gender = Gender.Male });
            Persons.Add(new Person() { Id = 4, FirstName = "Diana", LastName = "Doll", Age = 64, Gender = Gender.Female });

            Pets = new List<Pet>();
            Pets.Add(new Pet() { Id = 1, AnimalType = "Cat", Breed = "Turkish Angora", Name = "Lisa", Age = 2, Color = "White", Gender = Gender.Female, OwnerId = 2 });
            Pets.Add(new Pet() { Id = 2, AnimalType = "Dog", Breed = "Spaniel", Name = "Chuck", Age = 5, Color = "Brown", Gender = Gender.Male, OwnerId = 1 });
            Pets.Add(new Pet() { Id = 3, AnimalType = "Parrot", Breed = "African grey", Name = "Steely", Age = 3, Color = "Grey", Gender = Gender.Male, OwnerId = 1 });
            Pets.Add(new Pet() { Id = 4, AnimalType = "Guinea pig", Breed = "Texel", Name = "Fasty", Age = 1, Color = "Red", Gender = Gender.Female, OwnerId = 3 });
            Pets.Add(new Pet() { Id = 5, AnimalType = "Cat", Breed = "Siamese", Name = "Hamlet", Age = 3, Color = "Colorpoint", Gender = Gender.Male, OwnerId = 4 });
            Pets.Add(new Pet() { Id = 6, AnimalType = "Dog", Breed = "Caucasian Shepherd", Name = "Guardi", Age = 5, Color = "White", Gender = Gender.Female, OwnerId = 4 });

            //foreach (Pet pet in Pets)
            //{
            //    Persons.FirstOrDefault(p => p.Id == pet.OwnerId).Pets.Add(pet);
            //}
        }

        public Pet AddPet(Pet pet)
        {
            pet.Id = Pets.Max(p => p.Id) + 1;
            Pets.Add(pet);
            return pet;
        }

        public Pet GetPet(long id)
        {
            return Pets.FirstOrDefault(p => p.Id == id);
        }

        public void DeletePet(long id)
        {
            Pets.RemoveAll(p => p.Id == id);
        }

        public Pet UpdatePet(Pet pet)
        {
            Pet petInDb = Pets.FirstOrDefault(p => p.Id == pet.Id);
            if (petInDb != null)
            {
                petInDb.Age = pet.Age;
                petInDb.AnimalType = pet.AnimalType;
                petInDb.Breed = pet.Breed;
                petInDb.Color = pet.Color;
                petInDb.Name = pet.Name;
                petInDb.Gender = pet.Gender;
                petInDb.OwnerId = pet.OwnerId;
            }
            return petInDb;
        }

        public List<Pet> GetAllPets()
        {
            return Pets;
        }

        public Person AddPerson(Person person)
        {
            person.Id = Persons.Max(p => p.Id) + 1;
            Persons.Add(person);
            return person;
        }

        public Person GetPerson(long id)
        {
            return Persons.FirstOrDefault(p => p.Id == id);
        }

        public void DeletePerson(long id)
        {
            Persons.RemoveAll(p => p.Id == id);
        }

        public Person UpdatePerson(Person person)
        {
            Person personInDb = Persons.FirstOrDefault(p => p.Id == person.Id);
            if (personInDb != null)
            {
                personInDb.Age = person.Age;
                personInDb.FirstName = person.FirstName;
                personInDb.LastName = person.LastName;
                personInDb.Gender = person.Gender;
            }
            return personInDb;
        }

        public List<Person> GetAllPersons()
        {
            return Persons;
        }
    }
}
