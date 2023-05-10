using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TestServer.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace TestServer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class MeetController : ControllerBase
    {
        MyContext context;
        private List<MeetInternal>? FTFind(List<MeetInternal> list, TimeSpan duration)
        {
            List<MeetInternal> result = new();
            list.Sort((x, y) =>
            {
                if (x.start == y.start)
                {
                    if (x.end > y.end)
                    {
                        return 1;
                    }
                }
                else if (x.start < y.start)
                {
                    return -1;
                }
                return 1;
            });
            if (list.Count == 0) return new List<MeetInternal> { new MeetInternal(new TimeSpan(9,0,0), new TimeSpan(18, 0, 0)) };
            List<MeetInternal> listUpdated = new()
            {
                list[0]
            };
            list.Remove(list[0]);
            foreach (MeetInternal meet in list)
            {
                var templist = listUpdated.Where(x => { return x.start <= meet.start && x.end >= meet.end; }).ToList();
                if (templist.Count == 0) listUpdated.Add(meet);
                else
                {
                    listUpdated.Remove(templist[0]);
                    listUpdated.Add(new MeetInternal(
                        start: templist[0].start,
                        end: templist[0].end < meet.end ? meet.end : templist[0].end
                    ));
                }
            }
            if (listUpdated.Count == 0)
            {
                result.Add(new MeetInternal(new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0)));
                return result;
            }
            
            TimeSpan refs = new TimeSpan(9, 0, 0);
            TimeSpan refe = new TimeSpan(18, 0, 0);
            TimeSpan start = new TimeSpan(9, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);
            if (listUpdated.First().start < start)
            {
                start= listUpdated.First().end;
                if (start >= end) return result;
                listUpdated.Remove(listUpdated.First());
            }
            foreach (MeetInternal meet in listUpdated)
            {
                end = meet.start;
                if (end > refe)
                {
                    end = refe;
                    if (end - start >= duration)
                    {
                        TimeSpan beg = new TimeSpan(start.Ticks);
                        TimeSpan ovr = new TimeSpan(end.Ticks);
                        result.Add(new MeetInternal(beg, ovr));
                    }
                    return result;
                }
                if (end - start >= duration)
                {
                    TimeSpan beg = new TimeSpan(start.Ticks);
                    TimeSpan ovr = new TimeSpan(end.Ticks);
                    result.Add(new MeetInternal(beg, ovr));
                }
                start = meet.end;
                if (start > refe) return result;
            }
            if (start < refe && refe - start >= duration) result.Add(new MeetInternal(start, refe));  
            return result;
        }
        public MeetController(
            )
        {
            this.context = context;
        }

        [Authorize]
        [HttpPost("Meet")]
        public IActionResult PostMeet([FromBody]MeetDTO? data)
        {
            Console.WriteLine("Log: Start");
            if (data == null || data.date == null || data.faculty == null)
            {
                Console.WriteLine("Log: BadReq");
                return BadRequest();
            }
            var dateArray = data.date.Split('/');
            if (dateArray.Length < 2) return BadRequest();
            DateTime dt = new DateTime(Convert.ToInt32(dateArray[2]), Convert.ToInt32(dateArray[1]), Convert.ToInt32(dateArray[0]));                 
            List<MeetInternal> list = new List<MeetInternal>();
            foreach (var name in data.faculty)
            {
                Instructor? ins = context.Instructors.Include(x=>x.teaches).ThenInclude(x=>x.section).ThenInclude(x=> x.Event).ThenInclude(x=>x.occurences.Where(y=>y.day == (int)dt.DayOfWeek)).SingleOrDefault(x=>x.name== name);
                if (ins == null) continue;
                foreach (var tc in ins.teaches)
                {
                    foreach(var occr in tc.section.Event.occurences)
                    {
                        list.Add(new MeetInternal(occr.time_begin, occr.time_end));
                    }
                }
            }
            List<MeetInternal>? fin = FTFind(list, new TimeSpan(data.duration / 60, data.duration % 60, 0));
            if (fin == null) return NotFound();
            List<MeetResponseDTO> resp = new();
            foreach (var slts in fin)
            {
                resp.Add(new MeetResponseDTO(slts.start, slts.end));
            }
            Console.WriteLine("Log: Resp");
            return Ok(resp);
        }
    }
}
