namespace Api.DAL
{
    public class Person
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public Person(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}