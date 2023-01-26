using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Loader
{
    class LookUpEntry
    {
        public string CourseCode { get; set; }
        public string Name { get; set; }
        //public 
    }
    class TimeOccr
    {
        public TimeSpan time_begin { get; set; }
        public TimeSpan time_end { get; set; }
        public string ClassRoom { get; set; }
        public string Course { get; set; }
        public DayOfWeek day { get; set; }
    }
    internal class Schedule
    {
        public static Dictionary<string, List<TimeOccr>> lis = new Dictionary<string, List<TimeOccr>>();
        public static void readSchedule()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\TIME TABLE_Classroom_2022.htm");
            var tables = doc.DocumentNode.SelectNodes("//tables").Where(z => z.NodeType == HtmlNodeType.Element).ToList();
            tables.RemoveAt(0);
            for (int i = 0; i < tables.Count / 2; i++) { 
                
            }
        }
    }
}
