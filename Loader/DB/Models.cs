using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loader
{
    public class Instructor
    {
        public string instructor_id { get; set; }
        public string name { get; set; }
        [EmailAddress]
        public string email_id { get; set; }
        public string department { get; set; }
        public ICollection<Course> courses { get; set; }
        public ICollection<Instructor_Of> instrucor_of { get; set; }
        public ICollection<Teaches> teaches { get; set; }
        public ICollection<Event> events { get; set; }
    }
    public class Course
    {
        public string course_code { get; set; }
        public string course_name { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public float total_credits { get; set; }
        public float lecture_credits { get; set; }
        public float tutorial_credits { get; set; }
        public float practical_credits { get; set; }
        public string coordinator_id {get; set; }
        public string department { get; set; }
        public Instructor coordinator { get; set; }
        public ICollection<Instructor_Of> instrucor_of { get; set; }
        public ICollection<Section> sections { get; set; }
    }
    public class Instructor_Of
    {
        public string instrucor_id { get; set; }
        public string course_code { get; set; }
        public Instructor instructor { get; set; }
        public Course course { get; set; }
    }
    public class Section
    {        
        public string section_id { get; set; }
        public string course_code { get; set; }
        public int event_id { get; set; }
        public string name { get; set; }
        public Course course { get; set; }
        public ICollection <Teaches> teaches { get; set; }
        public Event Event { get; set; }
    }
    public class Teaches
    {
        public string instructor_id { get; set; }
        public string section_id { get; set; }
        public Instructor instructor { get; set; }
        public Section section{ get; set; }
    }

    public class Event
    {
        public int event_id { get; set; }
        public string name { get; set; }
        public bool is_course { get; set; }
        public bool ignore_holiday { get; set; }
        public string owner { get; set; }
#nullable enable
        public Section? section { get; set; }
#nullable disable
        public Instructor Owner { get; set; }
        public ICollection<Occurence> occurences { get; set; }
    }

    public class Occurence
    {
        public int occurence_id { get; set; }
        public int event_id { get; set; }
        public TimeSpan time_begin { get; set; }
        public TimeSpan time_end { get; set; }
        public int day { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_end { get; set; }
        public Event event_ { get; set; }
#nullable enable
        public string? room_code { get; set; }               
        public Room? room { get; set; }
#nullable disable 
    }

    public class Room
    {
        public string room_code { get; set; }
        public string building { get; set; }
        public string type { get; set; }
        public int capacity { get; set; }
        public ICollection<Occurence> occurences { get; set; }
    }
}
