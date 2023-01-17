namespace TestServer.Models
{
    public class EventDTO
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string room_code { get; set; }
        public string course_name { get; set; }
        public string section { get; set; }
        public string event_id { get; set; }
        public string occurence_id { get; set; }
    }

    public class DailyDTO
    {
        public DailyDTO()
        {
            event_list = new List<EventDTO>();
        }
        public IList<EventDTO> event_list { get; set; }
        public string day { get; set; }
        public string date { get; set; }
    }

    public enum MonthsDTO
    {
        January, February, March, April, May, June, July, August, September, October, November, December
    }

    public class DetailsDTO
    {
        public string category { get; set;}
        public string type { get; set;}
        public string department { get; set; }
        public string total_credits { get; set; }
        public string lecture_credits { get; set; }
        public string tutorial_credits { get; set; }
        public string practical_credits { get; set; }
        public string next_date { get; set; }
        public string next_day { get; set; }
        public string next_start_time { get; set; }
        public string next_end_time { get; set; }
    }

}
