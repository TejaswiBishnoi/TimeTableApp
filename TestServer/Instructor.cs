namespace TestServer
{
    public class InstructorDTO
    {
        public InstructorDTO(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
