namespace Domain
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public List<TheatreScreen> TheatreScreen { get; set; }
    }
}
