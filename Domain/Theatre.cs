namespace Domain
{
    public class Theatre
    {
        public Guid Id { get; set; }
        public string TheatreName { get; set; }
        public string Location { get; set; }
        public List<TheatreScreen> Screens { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
