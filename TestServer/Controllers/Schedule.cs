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
        [HttpGet("WeekD")]
        public IActionResult GetWeekFromDate(string date, string? name)
        {
            string? Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (name != null && name != "")
            {
                Console.WriteLine("Name:" + name);
                var x = context.Instructors.SingleOrDefault(x => EF.Functions.Like(x.name, $"{name}%"));
                if (x == null && name.Length >= 6)
                {
                    x = context.Instructors.SingleOrDefault(x => EF.Functions.Like(x.name, $"{name.Substring(0, 6)}%"));
                    if (x == null) return BadRequest();
                    Id = x.instructor_id;
                }
                else if (x == null) return BadRequest();
                else Id = x.instructor_id;
            }
            else Console.WriteLine("Old");
            var DateArray = date.Split('-');
            DateTime dt = new DateTime(Convert.ToInt32(DateArray[2]), Convert.ToInt32(DateArray[1]), Convert.ToInt32(DateArray[0]));
            DateTime dt2 = dt.AddDays(7);

            Dictionary<string, DateTime> ceq = new Dictionary<string, DateTime>();

            Instructor iss = context.Instructors.Single(s => s.instructor_id == Id);
            context.Entry(iss).Collection(s => s.teaches).Query().Include(s => s.section).ThenInclude(s => s.Event).Load();
            IList<Event> events = iss.teaches.Select(s => s.section).Select(s => s.Event).ToList();
            IList<Occurence> occr = new List<Occurence>();
            foreach (Event ev in events)
            {
                context.Entry(ev).Collection(s => s.occurences).Query().Where(s => DateTime.Compare(s.date_start, dt) <= 0 && DateTime.Compare(s.date_end, dt2) >= 0).ToList();
                occr = occr.Union(ev.occurences).ToList();
            }
            var occurDic = occr.GroupBy(s => s.day).ToDictionary(g => g.Key, g => g.ToList());

            List<DailyDTO> daily = new List<DailyDTO>();
            for (int i = 0; i < 7; i++)
            {
                ceq[dt.AddDays(i).DayOfWeek.ToString()] = dt.AddDays(i);
                DailyDTO d = new DailyDTO();
                d.day = ((DayOfWeek)i).ToString();
                if (!occurDic.ContainsKey(i))
                {
                    daily.Add(d);
                    continue;
                }
                foreach (Occurence oc in occurDic[i])
                {
                    EventDTO e = new EventDTO();
                    Event ee = oc.event_;
                    context.Entry(ee.section).Reference(s => s.course).Load();
                    e.course_name = ee.section.course.course_name;
                    e.section = ee.section.name;
                    e.start_time = oc.time_begin.ToString().Substring(0, 5);
                    e.end_time = oc.time_end.ToString().Substring(0, 5);
                    e.room_code = oc.room_code;
                    e.occurence_id = oc.occurence_id.ToString();
                    e.event_id = ee.event_id.ToString();
                    if (e.room_code == null) e.room_code = "";
                    d.event_list.Add(e);
                }
                d.event_list = d.event_list.OrderBy(s => s.start_time).ToList();
                daily.Add(d);
            }
            for (int i = 0; i < 7; i++)
            {
                daily[i].date = $"{ceq[daily[i].day].Day} {((MonthsDTO)(ceq[daily[i].day].Month - 1))} {ceq[daily[i].day].Year}";
            }
            daily.Sort((x, y) => DateTime.Compare(ceq[x.day], ceq[y.day]));
            Console.WriteLine("{0} : OG", DateTime.Now.ToShortTimeString());
            return Ok(daily);
        }

        [Authorize]
        [HttpGet("Week")]
        public IActionResult GetWeek()
        {
            Console.WriteLine(Request.Headers);
            Console.WriteLine("{0} : OGG", DateTime.Now.ToShortTimeString());
            Console.WriteLine(Response.Headers);
            string? Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dt2 = dt.AddDays(7);

            Dictionary<string, DateTime> ceq = new Dictionary<string, DateTime>();            

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
                ceq[dt.AddDays(i).DayOfWeek.ToString()] = dt.AddDays(i);
                DailyDTO d = new DailyDTO();
                d.day = ((DayOfWeek)i).ToString();
                if (!occurDic.ContainsKey(i))
                {                 
                    daily.Add(d);
                    continue;
                }
                foreach(Occurence oc in occurDic[i])
                {               
                    EventDTO e = new EventDTO();
                    Event ee = oc.event_;
                    context.Entry(ee.section).Reference(s => s.course).Load();
                    e.course_name = ee.section.course.course_name;
                    e.section = ee.section.name;
                    e.start_time = oc.time_begin.ToString().Substring(0,5);
                    e.end_time = oc.time_end.ToString().Substring(0,5);
                    e.room_code = oc.room_code;
                    e.occurence_id = oc.occurence_id.ToString();
                    e.event_id = ee.event_id.ToString();
                    if (e.room_code == null) e.room_code = "";
                    d.event_list.Add(e);
                }
                d.event_list = d.event_list.OrderBy(s => s.start_time).ToList();                
                daily.Add(d);
            }
            for (int i = 0; i < 7; i++)
            {
                daily[i].date = $"{ceq[daily[i].day].Day} {((MonthsDTO)(ceq[daily[i].day].Month - 1))} {ceq[daily[i].day].Year}";
            }
            daily.Sort((x, y) => DateTime.Compare(ceq[x.day], ceq[y.day]));
            Console.WriteLine("{0} : OG", DateTime.Now.ToShortTimeString());
            return Ok(daily);
        }


        //
        //

        [Authorize]
        [HttpGet("CWeek")]
        public IActionResult GetCWeek(string? code, string? date)
        {
            if (code == null || date == null) return BadRequest();

            Console.WriteLine("{0} : OGG", DateTime.Now.ToShortTimeString());
            var DateArray = date.Split('-');
            DateTime dt = new DateTime(Convert.ToInt32(DateArray[2]), Convert.ToInt32(DateArray[1]), Convert.ToInt32(DateArray[0]));
            DateTime dt2 = dt.AddDays(7);

            Dictionary<string, DateTime> ceq = new Dictionary<string, DateTime>();

            Room? rm = context.Rooms.SingleOrDefault(s => s.room_code == code);
            if(rm == null)
            {
                return NotFound();
            }
            context.Entry(rm).Collection(s => s.occurences).Query().Include(s => s.event_).ThenInclude(s => s.section).Load();
            IList<Occurence> ocr = rm.occurences.Where(s => DateTime.Compare(s.date_start, dt) <= 0 && DateTime.Compare(s.date_end, dt2) >= 0).ToList();
            
            var occurDic = ocr.GroupBy(s => s.day).ToDictionary(g => g.Key, g => g.ToList());

            List<CDailyDTO> daily = new List<CDailyDTO>();
            for (int i = 0; i < 7; i++)
            {
                ceq[dt.AddDays(i).DayOfWeek.ToString()] = dt.AddDays(i);
                CDailyDTO d = new CDailyDTO();
                d.day = ((DayOfWeek)i).ToString();
                if (!occurDic.ContainsKey(i))
                {
                    daily.Add(d);
                    continue;
                }
                foreach (Occurence oc in occurDic[i])
                {
                    CEventDTO e = new CEventDTO();
                    Event ee = oc.event_;
                    context.Entry(ee.section).Reference(s => s.course).Load();
                    context.Entry(ee.section).Collection(s => s.teaches).Query().Include(s => s.instructor).Load();
                    e.course_name = ee.section.course.course_name;
                    e.section = ee.section.name;
                    e.start_time = oc.time_begin.ToString().Substring(0, 5);
                    e.end_time = oc.time_end.ToString().Substring(0, 5);
                    //e.room_code = oc.room_code;
                    e.occurence_id = oc.occurence_id.ToString();
                    e.event_id = ee.event_id.ToString();
                    e.instructor = "";
                    foreach (var tc in ee.section.teaches)
                    {
                        e.instructor += tc.instructor.name + ", ";
                    }
                    if (e.instructor.Length > 0)
                    e.instructor = e.instructor.Substring(0, e.instructor.Length - 2);
                    //if (e.room_code == null) e.room_code = "";
                    d.event_list.Add(e);
                }
                d.event_list = d.event_list.OrderBy(s => s.start_time).ToList();
                daily.Add(d);
            }
            for (int i = 0; i < 7; i++)
            {
                daily[i].date = $"{ceq[daily[i].day].Day} {((MonthsDTO)(ceq[daily[i].day].Month - 1))} {ceq[daily[i].day].Year}";
            }
            daily.Sort((x, y) => DateTime.Compare(ceq[x.day], ceq[y.day]));
            Console.WriteLine("{0} : OG", DateTime.Now.ToShortTimeString());
            return Ok(daily);
        }

        //
        //


        [HttpGet("EventDetails")]
        [Authorize]
        public IActionResult GetDetails(string eventID, string occurenceID)
        {
            Console.WriteLine("{0}: Rec", DateTime.Now.ToLongTimeString());
            string? Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Id == null) return BadRequest();
            Event? e = context.Events.Where(s => s.event_id.ToString() == eventID).Include(s => s.section).ThenInclude(s => s.course).Include(s => s.occurences).FirstOrDefault();
            Occurence? o = e.occurences.Where(s => s.occurence_id.ToString() == occurenceID).FirstOrDefault();
            if (e == null || o == null) return BadRequest("No Event with event id: " + eventID);
            DetailsDTO dto = new();
            if (e.section is null) return BadRequest();
            Course c = e.section.course;
            dto.department = c.department;
            dto.category = c.category;
            dto.type = c.type;
            dto.total_credits = c.total_credits.ToString();
            dto.practical_credits = c.practical_credits.ToString();
            dto.tutorial_credits = c.tutorial_credits.ToString();
            dto.lecture_credits = c.lecture_credits.ToString();
            //Known Bug - Today's date being taken instead of the schedule's
            Occurence? nextoc;
            DateTime dat = DateTime.Now;
            nextoc = e.occurences.Where(s => s.day == o.day && s.time_begin >= o.time_end).OrderBy(s => s.time_begin).FirstOrDefault();
            if (nextoc is null)
            {
                int dt = o.day;                
                for (int i = 1; i < 7; i++)
                {
                    dt += 1;
                    dt %= 7;
                    nextoc = e.occurences.Where(s => s.day == dt).Where(s => s.date_end >= dat.AddDays(i) && s.date_start <= dat.AddDays(i)).OrderBy(s => s.time_begin).FirstOrDefault();
                    if (nextoc is not null)
                    {
                        dat = dat.AddDays(i);
                        break;
                    }
                }               
            }
            if (nextoc is null)
            {
                dto.next_date = "-";
                dto.next_day = " ";
                dto.next_start_time = "-";
                dto.next_end_time = "-";
            }
            else
            {
                dto.next_day = ((DayOfWeek)nextoc.day).ToString();
                dto.next_start_time = nextoc.time_begin.ToString();
                dto.next_end_time = nextoc.time_end.ToString();
                dto.next_date = $"{dat.Day} {(MonthsDTO)dat.Month - 1} {dat.Year}";
            }
            Console.WriteLine("{0}: Res", DateTime.Now.ToLongTimeString());
            return Ok(dto);
        }

        [HttpGet("WeekT")]
        public IActionResult GetScheduleData()
        {
            DailyDTO dt = new();
            List<DailyDTO> daily = new List<DailyDTO>();
            EventDTO ev = new();
            ev.course_name = "Test";
            Console.WriteLine(Request.Headers);
            ev.start_time = DateTime.Now.ToShortTimeString();
            ev.end_time = DateTime.Now.ToShortTimeString();
            ev.section = "Book";
            ev.room_code = "D-OI";
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
