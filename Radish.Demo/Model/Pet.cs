namespace Radish.Demo.Model
{
    public class Pet
    {
        public long     Id { get; set; }
        public string   AnimalType { get; set; }
        public string   Name { get; set; }
        public string   Breed { get; set; }
        public int      Age  { get; set; }
        public string   Color { get; set; }
        public Gender   Gender  { get; set; }
        public long OwnerId { get; set; }
    }
}
