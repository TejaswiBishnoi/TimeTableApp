namespace TestServer.Models
{
    public class MeetInternal
    {
        public TimeSpan start { get; }
        public TimeSpan end { get; }
        public MeetInternal(TimeSpan start, TimeSpan end)
        {
            this.start = start;
            this.end = end;
        }
    }

    public class MeetDTO
    {
        public List<string>? faculty { get; set; }
        public string? date { get; set; }
        public int duration { get; set; }
    }

    public class MeetResponseDTO
    {
        public string start { get; set; }
        public string end { get; set; }
        public MeetResponseDTO(TimeSpan start, TimeSpan end)
        {
            this.start = $"{start.Hours}:{start.Minutes}";
            this.end = $"{end.Hours}:{end.Minutes}";
        }
    }
}
