using System.Collections.Generic;

namespace Radish.Demo.Model
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint Age { get; set; }
        public List<Pet> Pets { get; set; }
        public Gender Gender { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }
}
