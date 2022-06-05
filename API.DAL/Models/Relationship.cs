namespace Api.DAL
{
    public class Relationship
    {
        public string Source { get; set; }
        public string Kind { get; set; }
        public string Target { get; set; }
        public Relationship(string source, string kind, string target)
        {
            Source = source;
            Kind = kind;
            Target = target;
        }
    }
}