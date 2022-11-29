using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestServer.Models;

namespace TestServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Schedule : ControllerBase
    {
        MyContext context;
        public Schedule(MyContext context)
        {
            this.context = context;
        }
        [Authorize]
        [HttpGet("Week")]
        public IActionResult GetWeek()
        {
            string? Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dt2 = dt.AddDays(7);            

            Instructor iss = context.Instructors.Single(s => s.instructor_id == Id);
            context.Entry(iss).Collection(s => s.teaches).Query().Include(s => s.section).ThenInclude(s => s.Event).Load();
            IList <Event> events = iss.teaches.Select(s => s.section).Select(s => s.Event).ToList();
            IList<Occurence> occr = new List<Occurence>();
            foreach (Event ev in events)
            {
                context.Entry(ev).Collection(s => s.occurences).Query().Where(s => DateTime.Compare(s.date_start, dt) <= 0 && DateTime.Compare(s.date_end, dt2) >= 0).ToList();
                occr = occr.Union(ev.occurences).ToList();
            }
            var occurDic = occr.GroupBy(s => s.day).ToDictionary(g => g.Key, g=>g.ToList());
            
            List<DailyDTO> daily = new List<DailyDTO>();
            for (int i = 0; i < 7; i++)
            {
                DailyDTO d = new DailyDTO();
                d.event_list = new List<EventDTO>();
                if (!occurDic.ContainsKey((int)dt.DayOfWeek))
                {                    
                    daily.Add(d);
                    continue;
                }
                foreach(Occurence oc in occurDic[(int)dt.DayOfWeek])
                {               
                    EventDTO e = new EventDTO();
                    Event ee = oc.event_;
                    context.Entry(ee.section).Reference(s => s.course).Load();
                    e.course_name = ee.section.course.course_name;
                    e.section = ee.section.name;
                    e.start_time = oc.time_begin.ToString();
                    e.end_time = oc.time_end.ToString();
                    e.room_code = oc.room_code;
                    if (e.room_code == null) e.room_code = "";
                    d.event_list.Add(e);
                }
                daily.Add(d);
            }
            
            return Ok(daily);
        }
        [HttpGet("WeekT")]
        public IActionResult GetScheduleData()
        {
            DailyDTO dt = new();
            List<DailyDTO> daily = new List<DailyDTO>();
            EventDTO ev = new();
            ev.course_name = "Test";
            ev.start_time = DateTime.Now.ToShortTimeString();
            ev.end_time = DateTime.Now.ToShortTimeString();
            ev.section = "boob";
            ev.room_code = "Dildo";
            dt.event_list = new List<EventDTO>();
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.event_list.Add(ev);
            dt.day = DayOfWeek.Monday.ToString();
            MonthsDTO mt = MonthsDTO.November;
            dt.date = $"{DateTime.Now.Day.ToString()} {mt.ToString()} {DateTime.Now.Year.ToString()}";
            daily.Add(dt);
            daily.Add(dt);
            daily.Add(dt);
            daily.Add(dt);
            daily.Add(dt);
            daily.Add(dt);
            daily.Add(dt);
            Console.WriteLine(Request.Headers["Authorization"]);
            Console.WriteLine("gg");
            return Ok(daily);
        }
    }
}
