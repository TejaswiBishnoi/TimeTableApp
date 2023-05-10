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
            string hrs = start.Hours.ToString().Length != 2 ? "0" + start.Hours.ToString() : start.Hours.ToString();
            string min = start.Minutes.ToString().Length != 2 ? "0" + start.Minutes.ToString() : start.Minutes.ToString();
            this.start = $"{hrs}:{min}";
            hrs = end.Hours.ToString().Length != 2 ? "0" + end.Hours.ToString() : end.Hours.ToString();
            min = end.Minutes.ToString().Length != 2 ? "0" + end.Minutes.ToString() : end.Minutes.ToString();
            this.end = $"{hrs}:{min}";
        }
    }
}
