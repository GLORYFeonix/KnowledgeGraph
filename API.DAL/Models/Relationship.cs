namespace Api.DAL
{
    public class Relationship
    {
        // public string Name { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public Relationship(string source, string target)
        {
            // Name = name;
            Source = source;
            Target = target;
        }
    }
}