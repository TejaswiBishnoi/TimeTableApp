using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Loader.DB;

namespace Loader
{
    class CourseTemp
    {        
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public float lec, prac, tutor, total;
        public string coord { get; set; } = null!;
        public string Dept { get; set; } = null!;
        public string cat { get; set; } = null!;
        public string typ { get; set; } = null!;

    }
    internal class InstructorAdd
    {
        public static Dictionary<string, string> lis = new Dictionary<string, string>();
        public static List<CourseTemp> lis_ = new(); //Instructor ID -> Instructor Name;
        public static Dictionary<string, List<string>> isof = new();
        public static timetable2Context tt = new();
        public static void ParseData(string data)
        {
            var x = data.Split(',').Select(s => s.Trim());
            foreach(string xx in x)
            {
                var z = xx;
                z = z.Replace("\n ", "").Replace("\r", "");
                if(z.StartsWith("Course Coordinator: "))
                {
                    var zz = z.Substring("Course Coordinator: ".Length);
                    var zzz = zz.Split("(");
                    zz = zzz.FirstOrDefault();
                    if (zz == null) continue;
                    zz = zz.Trim();
                    var zx = zzz[1].Substring(0, zzz[1].Length - 1);
                    if (!lis.ContainsKey(zx)) lis[zx] = zz;
                }
                else if (z.StartsWith("Instructor:"))
                {
                    var zz = z.Substring("Instructor:".Length);
                    var zzz = zz.Split("(");
                    zz = zzz.FirstOrDefault();
                    if (zz == null) continue;
                    zz = zz.Trim();
                    var zx = zzz[1].Substring(0, zzz[1].Length - 1);
                    if (!lis.ContainsKey(zx)) lis[zx] = zz;
                }
                else
                {
                    return;
                }
            }
        }
        public static void AddInstructor()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\cd.htm");
            var rows = doc.DocumentNode.SelectNodes(".//tr");
            for (int i = 2; i< rows.Count; i++)
            {
                var row = rows[i];
                var cols = row.Elements("td").Where(z => z.NodeType == HtmlNodeType.Element);
                if (cols.Count() == 0) continue;
                ParseData(cols.Last().InnerText);
                if (cols.First().Attributes["rowspan"] != null && cols.First().Attributes["rowspan"].Value != "1")
                {
                    int ff = Convert.ToInt32(cols.First().Attributes["rowspan"].Value);
                    for (int j = 1; j< ff; j++)
                    {
                        i++;
                        row = rows[i];
                        var col = row.Elements("td").Where(z => z.NodeType == HtmlNodeType.Element).FirstOrDefault();
                        if (col == null) continue;
                        ParseData(col.InnerText);
                    }
                    i--;
                }
            }
            foreach (var x in lis.OrderBy(x => x.Value))
            {
                if (!tt.Instructors.Any(y => y.InstructorId == x.Key))
                {
                    Instructor ins = new Instructor();
                    ins.Name = x.Value;
                    ins.InstructorId= x.Key;
                    ins.EmailId = x.Key + "@iitjammu.ac.in";
                    ins.Department = "";
                    tt.Instructors.Add(ins);
                    Console.WriteLine("Ins");
                }
                Console.WriteLine("{0} | {1}", x.Key, x.Value);
            }
            tt.SaveChanges();
        }
        public static void addCourse()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\cd.htm");
            var rows = doc.DocumentNode.SelectNodes(".//tr");
            for (int i = 2; i < rows.Count; i++)
            {
                var row = rows[i];
                var cols = row.Elements("td").Where(z => z.NodeType == HtmlNodeType.Element).ToList();
                if (cols == null) continue;
                if (cols[0].InnerText == "") continue;
                CourseTemp c = new CourseTemp()
                {
                    Id = cols[1].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", ""),
                    Name = cols[2].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", ""),
                    cat = cols[3].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", ""),
                    typ = cols[4].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", ""),
                    total = (float)Convert.ToDouble(cols[5].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", "")),
                    lec = (float)Convert.ToDouble(cols[6].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", "")),
                    tutor = (float)Convert.ToDouble(cols[7].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", "")),
                    prac = (float)Convert.ToDouble(cols[8].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", ""))
                };                     
                if (cols[9].InnerText ==null || cols[9].InnerText == "")
                {
                    c.coord = "IITJMU000";
                }
                else
                {
                    var temps = cols[9].InnerText.Replace("\r", "").Replace("\n ", "").Replace("\n", "").Trim();
                    if (temps.StartsWith("Course Coordinator:"))
                    {
                        c.coord = temps.Split('(').Last();
                        c.coord = c.coord.Substring(0, c.coord.Length - 1).Trim();
                    }
                    else c.coord = "IITJMU000";
                }
                if (cols.First().Attributes["rowspan"] != null && cols.First().Attributes["rowspan"].Value != "1")
                {
                    int ff = Convert.ToInt32(cols.First().Attributes["rowspan"].Value);
                    i += ff - 1;
                }
                lis_.Add(c);
            }
            foreach (var o in lis_)
            {
                if (!tt.Courses.Any(x => x.CourseCode == o.Id))
                {
                    Course c = new Course();
                    c.CourseCode = o.Id;
                    c.CourseName = o.Name;
                    c.LectureCredits = Convert.ToDecimal(o.lec);
                    c.PracticalCredits = Convert.ToDecimal(o.prac);
                    c.TutorialCredits = Convert.ToDecimal(o.tutor);
                    c.TotalCredits = Convert.ToDecimal(o.total);
                    c.Type = o.typ;
                    c.Category = o.cat;
                    c.CoordinatorId = o.coord;
                    c.Department = "";
                    tt.Courses.Add(c);
                    Console.WriteLine("Ins");
                }
                Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", o.Id, o.Name, o.total, o.typ, o.cat, o.coord);
            }
            tt.SaveChanges();
        }
        public static void addInstructorOf()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\cd.htm");
            var rows = doc.DocumentNode.SelectNodes(".//tr");
            for (int i = 2; i < rows.Count; i++)
            {
                var row = rows[i];
                var cols = row.Elements("td").Where(z => z.NodeType == HtmlNodeType.Element).ToList();
                if (cols == null) continue;
                if (cols[0].InnerText == "") continue;
                var crs = cols[1].InnerText.Trim().Replace("\n ", "").Replace("\r", "").Replace("\n", "");
                isof[crs] = new List<string>();
                ParseData(cols[9].InnerText, crs);
                if (cols.First().Attributes["rowspan"] != null && cols.First().Attributes["rowspan"].Value != "1")
                {
                    int ff = Convert.ToInt32(cols.First().Attributes["rowspan"].Value);
                    for (int j = 1; j < ff; j++)
                    {
                        i++;
                        row = rows[i];
                        var col = row.Elements("td").Where(z => z.NodeType == HtmlNodeType.Element).FirstOrDefault();
                        if (col == null || col.InnerText == "") continue;
                        ParseData(col.InnerText, crs);
                    }
                }
            }
            foreach(var tt in isof)
            {
                Console.Write(tt.Key + " =>");
                foreach(var ttt in tt.Value)
                {
                    Console.Write(" " + ttt);
                }
                Console.WriteLine();
            }
        }
        public static void ParseData(string data, string crs)
        {
            var x = data.Split(',').Select(s => s.Replace("\n ", "").Replace("\r", "").Replace("\n", "").Trim());
            foreach (string xx in x)
            {
                var z = xx;
                if (z.StartsWith("Course Coordinator: "))
                {
                    var zz = z.Split('(').LastOrDefault();
                    if (zz == null) continue;
                    zz = zz.Substring(0, zz.Length- 1);
                    isof[crs].Add(zz);
                }
                else if (z.StartsWith("Instructor:"))
                {
                    var zz = z.Split('(').LastOrDefault();
                    if (zz == null) continue;
                    zz = zz.Substring(0, zz.Length - 1);
                    isof[crs].Add(zz);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
