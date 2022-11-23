namespace TestServer
{
    public class InstructorDTO
    {
        public InstructorDTO(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
