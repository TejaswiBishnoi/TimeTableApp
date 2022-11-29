namespace TestServer.Models
{
    public class EventDTO
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string room_code { get; set; }
        public string course_name { get; set; }
        public string section { get; set; }
    }

    public class DailyDTO
    {
        public IList<EventDTO> event_list { get; set; }
        public string day { get; set; }
        public string date { get; set; }
    }

    public enum MonthsDTO
    {
        January, February, March, April, May, June, July, August, September, October, November, December
    }
}
