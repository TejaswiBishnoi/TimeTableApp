using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace Loader
{
    class Lookup
    {
        public string Slot { get; set; }
        public string Name { get; set; }
        public List<string> Faculty;
        public string? Room { get; set; } = null;
        public string CCode { get; set; }
        public Lookup()
        {
            Faculty= new List<string>();
        }
    }
    class TimeOccr
    {
        public TimeSpan time_begin { get; set; }
        public TimeSpan time_end { get; set; }
        public string ClassRoom { get; set; }
        public string Course { get; set; }
        public DayOfWeek day { get; set; }
    }

    class Slots
    {
        public int[] Begin = new int[2];
        public int[] End = new int[2];
        public int begspan { get; set; }
        public int endspan {get; set;}
        public Slots() { }
    }
    class OneClass
    {
        public string SlotName { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int day { get; set; }
        public int span { get; set; } = 0;
    }
    internal class Schedule
    {
        public static int modee = 0;
        public static Dictionary<string, List<TimeOccr>> lis = new Dictionary<string, List<TimeOccr>>();
        public static void AddDB(List<Lookup> Global, List<Lookup> OE, List<List<Lookup>> Locals, List<List<OneClass>> ols)
        {
            MyContext context = new();
            for (int i = 0; i < ols.Count; i++)
            {
                HashSet<string> dic = new();
                for (int j = 0; j < ols[i].Count; j++)
                {
                    //if (context.Courses.Where(x=>x.course_code == ols[i][j].SlotName).Any())
                    //{
                    //    if (context.Sections.Where(x=>x.course.course_code == ols[i][j].SlotName).Any())
                    //    {
                    //        var sec = context.Sections.Include(x => x.teaches).Include(x=>x.Event).ThenInclude(x=>x.occurences).Where(x => x.course.course_code == ols[i][j].SlotName).ToList();
                    //        foreach (var z in sec)
                    //        {

                    //        }
                    //    }
                    //}
                    if (false) ;
                    else
                    {                        
                        List<Lookup> list = new List<Lookup>();
                        list.AddRange(Locals[i].Where(x => x.Slot == ols[i][j].SlotName));
                        list.AddRange(Global.Where(x => x.Slot == ols[i][j].SlotName));
                        list.AddRange(OE.Where(x => x.Slot == ols[i][j].SlotName));
                        foreach (var z in list)
                        {
                            //Console.WriteLine("Faculty: " + z.Faculty.FirstOrDefault() + " " + z.CCode + " " + ols[i][j].SlotName);
                            if (dic.Contains(z.CCode)) continue;
                            if (!context.Courses.Where(x => x.course_code == z.CCode).Any()) continue;
                            var sec = context.Sections.Include(x => x.teaches).Include(x => x.Event).ThenInclude(x => x.occurences).Where(x => x.course.course_code == z.CCode).ToList();
                            //if (sec.Count == 0)
                            //{
                            //    Event e = new Event();
                            //    e.owner = "IITJMU000";
                            //    e.event_id = context.Events.Count();
                            //    e.name = "Class";
                            //    context.Events.Add(e);
                            //    context.SaveChanges();
                            //    Occurence oc = new Occurence();
                            //    oc.date_start = new DateTime(2023, 1, 1);
                            //    oc.date_end = new DateTime(2023, 5, 1);
                            //    oc.time_begin = ols[i][j].TimeBegin;
                            //    oc.time_end = ols[i][j].TimeEnd;
                            //    oc.event_id = e.event_id;
                            //    if (context.Rooms.Where(f => f.room_code == z.Room).Any())
                            //    {
                            //        oc.room_code = z.Room;
                            //    }
                            //    context.Occurences.Add(oc);
                            //    context.SaveChanges();

                            //    Section se = new();
                            //    se.course_code = z.CCode;
                            //    se.section_id = context.Sections.Count().ToString();
                            //    se.event_id = e.event_id;
                            //    context.Sections.Add(se);
                            //    context.SaveChanges();
                                

                            //    foreach (var fac in z.Faculty)
                            //    {
                            //        Teaches tc = new Teaches();
                            //        tc.instructor_id = fac;
                            //        tc.section_id = se.section_id;
                            //        context.Teaches_.Add(tc);
                            //    }
                            //    context.SaveChanges();

                            //}
                            bool ons = false;
                            foreach (var zz in sec)
                            {
                                bool acc = true;
                                foreach (var fac in z.Faculty)
                                {
                                    if (!zz.teaches.Where(x => x.instructor_id == fac).Any())
                                    {
                                        acc = false; break;
                                    }
                                }
                                //if (!acc)
                                //{
                                //    Event e = new Event();
                                //    e.owner = "IITJMU000";
                                //    e.event_id = context.Events.Count();

                                //    context.Events.Add(e);

                                //    Occurence oc = new Occurence();
                                //    oc.date_start = new DateTime(2023, 1, 1);
                                //    oc.date_end = new DateTime(2023, 5, 1);
                                //    oc.time_begin = ols[i][j].TimeBegin;
                                //    oc.time_end = ols[i][j].TimeEnd;
                                //    oc.event_id = e.event_id;
                                //    if (context.Rooms.Where(f => f.room_code == z.Room).Any())
                                //    {
                                //        oc.room_code = z.Room;
                                //    }
                                //    context.Occurences.Add(oc);
                                //    context.SaveChanges();
                                //}
                                if (acc)
                                {
                                    
                                    ons = true;
                                    if (!zz.Event.occurences.Where(cv => cv.time_begin == ols[i][j].TimeBegin && cv.time_end == ols[i][j].TimeEnd && cv.day == ols[i][j].day).Any())
                                    {
                                        Occurence oc = new Occurence();
                                        oc.date_start = new DateTime(2023, 1, 1);
                                        oc.date_end = new DateTime(2023, 5, 1);
                                        oc.time_begin = ols[i][j].TimeBegin;
                                        oc.time_end = ols[i][j].TimeEnd;
                                        oc.event_id = zz.event_id;
                                        oc.day = ols[i][j].day;
                                        if (context.Rooms.Where(f => f.room_code == z.Room).Any())
                                        {
                                            oc.room_code = z.Room;
                                        }
                                        context.Occurences.Add(oc);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            if (!ons)
                            {
                                Event e = new Event();
                                e.owner = "IITJMU000";
                                e.event_id = context.Events.Count() + 1;
                                e.name = "Class";
                                context.Events.Add(e);
                                context.SaveChanges();
                                Occurence oc = new Occurence();
                                oc.date_start = new DateTime(2023, 1, 1);
                                oc.date_end = new DateTime(2023, 5, 1);
                                oc.time_begin = ols[i][j].TimeBegin;
                                oc.time_end = ols[i][j].TimeEnd;
                                oc.event_id = e.event_id;
                                oc.day = ols[i][j].day;
                                if (context.Rooms.Where(f => f.room_code == z.Room).Any())
                                {
                                    oc.room_code = z.Room;
                                }
                                context.Occurences.Add(oc);
                                context.SaveChanges();

                                Section se = new();
                                se.course_code = z.CCode;
                                se.section_id = context.Sections.Count().ToString();

                                se.event_id = e.event_id;
                                se.name = " aa";
                                context.Sections.Add(se);
                                context.SaveChanges();

                                foreach (var fac in z.Faculty)
                                {
                                    Teaches tc = new Teaches();
                                    tc.instructor_id = fac;
                                    tc.section_id = se.section_id;
                                    context.Teaches_.Add(tc);
                                }
                                context.SaveChanges();

                            }
                        }
                    }
                }
            }
        }
        public static List<Lookup> BuildLookUp(ref int rw, HtmlNodeCollection rows)
        {
            List<Lookup> table = new List<Lookup>();
            for (;rw < rows.Count; rw++)
            {
                var row = rows[rw];
                var cols = row.SelectNodes("./td");
                if (cols == null) break;
                Lookup entr = new();
                bool aten = false;
                for (int i = 0; i < cols.Count && i < 6; i++)
                {
                    var col = cols[i];
                    string text_col = "";
                    if (col.InnerText != null) text_col = (col.InnerText);
                    Regex regex = new Regex(@"\s+");
                    text_col = regex.Replace(text_col, " ").Trim();
                    if (i == 0 && (col.InnerText == null || col.InnerText == "" || col.InnerText.Contains("follows")))
                    {
                        aten= true;
                        break;
                    }
                    if (text_col == "&nbsp;") continue;
                    if (i == 0) entr.Slot = text_col;
                    else if (i == 1) entr.CCode= text_col;
                    else if (i == 2) entr.Name= text_col;
                    else if (i == 4)
                    {
                        var facs = text_col.Trim().Split(',').Where(x => x != "").Select(x => x.Trim()).ToList();
                        MyContext ctx = new();
                        foreach (var zz in facs)
                        {
                            string? Id = ctx.Instructors.Where(x=>x.name == zz).Select(x=>x.instructor_id).FirstOrDefault();
                            if (Id != null) entr.Faculty.Add(Id);
                        }                        
                    }
                    else if (i == 5)
                    {
                        var rooms = text_col.Trim().Split(',').Where(x => x != "").Select(x => x.Trim()).ToList();
                        if (rooms.Count > 0) entr.Room = rooms[0];
                        string? colss = col.Attributes["rowspan"]?.Value;
                        if (colss == null) colss = "1";
                        rw += Convert.ToInt32(colss) - 1;
                    }
                }
                if (aten) break;
                table.Add(entr);
            }
            return table;
        }
        public static List<OneClass> LocSched(ref int rw, HtmlNodeCollection rows)
        {
            List<OneClass> table = new();
            List<Slots> slots = new();
            var hdrow = rows[rw++];
            var hdrcols = hdrow.SelectNodes("./td");
            if (hdrcols == null) return table;
            int hcolspan = 0;
            foreach (var col in hdrcols)
            {
                string? colss = col.Attributes["colspan"]?.Value;
                if (colss == null) colss = "1";
                string text_col = "";
                if (col.InnerText != null) text_col = (col.InnerText);
                if (text_col.Length == 0) break;
                if (text_col == "&nbsp;")
                {
                    hcolspan += Convert.ToInt32(colss);
                    continue;
                }
                var z = text_col.Trim().Split('-');
                var z0 = z[0].Split('.');
                var z1 = z[1].Split('.');
                int z00 = Convert.ToInt32(z0[0]);
                int z01 = (z0.Length == 2) ? Convert.ToInt32(z0[1]) : 0;
                if (z01 >= 60)
                {
                    z01 %= 60;
                    z00++;
                }
                int z10 = Convert.ToInt32(z1[0]);
                int z11 = Convert.ToInt32(z1[1]);
                z11 += 10;
                if (z11 >= 60)
                {
                    z11 %= 60;
                    z10++;
                }
                Slots slt = new Slots();
                slt.Begin[0] = z00;
                slt.Begin[1] = z01;
                slt.End[0] = z10;
                slt.End[1] = z11;
                slt.begspan = hcolspan;
                slt.endspan = hcolspan + Convert.ToInt32(colss);
                slots.Add(slt);
                hcolspan = slt.endspan;
            }
            for (; rw < rows.Count; rw++)
            {
                var row = rows[rw];
                var cols = row.SelectNodes("./td");
                if (cols == null) continue;
                int i = 0;
                int colspan = 0;
                string Week = "";
                foreach(var col in cols )
                {
                    string? colss = col.Attributes["colspan"]?.Value;
                    if (colss == null) colss = "1";
                    string text_col = "";
                    if (col.InnerText != null) text_col = (col.InnerText);
                    if (text_col == "") break;
                    if (i == 0)
                    {
                        Week = text_col;
                        colspan += Convert.ToInt32(colss);
                        i++;
                    }
                    else
                    {
                        if (text_col == "&nbsp;")
                        {
                            colspan += Convert.ToInt32(colss);
                            continue;
                        }
                        OneClass cl = new OneClass();
                        cl.span = Convert.ToInt32(colss);
                        int collll = Convert.ToInt32(colss) + colspan;
                        Slots? beg = slots.Where(x => x.begspan <= colspan && x.endspan > colspan).FirstOrDefault();
                        if (beg == null) { Console.WriteLine("ERROR"); }
                        Slots? end = slots.Where(x => x.begspan < collll && x.endspan >= collll).FirstOrDefault();
                        if (colspan != beg.begspan || collll != end.endspan)
                        {
                            var a1 = new TimeSpan(beg.Begin[0], beg.Begin[1], 0);
                            var a2 = new TimeSpan(beg.End[0], beg.End[1], 0);
                            var b1 = new TimeSpan(end.Begin[0], end.Begin[1], 0);
                            var b2 = new TimeSpan(end.End[0], end.End[1], 0);
                            a1 = a1 + (a2 - a1) * (colspan - beg.begspan) / (beg.endspan - beg.begspan);
                            b1 = b1 + (b2 - b1)*(collll- end.begspan)/(end.endspan - end.begspan);
                            cl.TimeBegin = a1;
                            cl.TimeEnd = b1;
                        }
                        else
                        {
                            cl.TimeBegin = new TimeSpan(beg.Begin[0], beg.Begin[1], 0);
                            cl.TimeEnd = new TimeSpan(end.End[0], end.End[1], 0);
                        }            
                        
                        switch (Week)
                        {
                            case "MON":
                                cl.day = 1;
                                break;
                            case "TUE":
                                cl.day = 2;
                                break;
                            case "WED":
                                cl.day = 3;
                                break;
                            case "THU":
                                cl.day = 4;
                                break;
                            case "FRI":
                                cl.day = 5;
                                break;
                        }
                        cl.SlotName = text_col.Split('(')[0].Trim();
                        table.Add(cl);
                        colspan = collll;
                    }
                }
                if (Week == "") break;
            }
            return table;
        }
        public static void read(bool Ic = true)
        {
            Dictionary<string, List<TimeOccr>> lis = new Dictionary<string, List<TimeOccr>>();
            int mode = 0;
            MyContext context = new MyContext();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\attachments\MT.htm");
            var tables = doc.DocumentNode.SelectNodes("//table");
            if (tables == null) return;
            if (tables.Count == 0) return;
            var table = tables.FirstOrDefault();
            var rows = table.SelectNodes("./tr");
            List<Slots> slots = new List<Slots>();
            string Week = "";
            List<OneClass> liss = new List<OneClass>();
            int rw = 0;
            List<Lookup> Global;
            if (Ic)
            {
                foreach (var row in rows)
                {
                    var cols = row.SelectNodes("./td");
                    if (cols == null) continue;
                    int i = 0;
                    int colspan = 0;
                    foreach (var col in cols)
                    {
                        string? colss = col.Attributes["colspan"]?.Value;
                        if (colss == null) colss = "1";
                        string text_col = "";
                        if (col.InnerText != null) text_col = (col.InnerText);
                        Regex regex = new Regex(@"\s+");
                        text_col = regex.Replace(text_col, " ").Trim();
                        if (mode == 0)
                        {
                            if (text_col != "List of Institute Core (IC) and Credit Earning (CE) courses and their slots")
                            {
                                break;
                            }
                            else
                            {
                                mode++;
                                break;
                            }
                        }
                        else if (mode == 1)
                        {
                            if (text_col == "Slot") mode++;
                            break;
                        }
                    }
                    rw++;
                    if (mode == 2) break;
                }
                Global = BuildLookUp(ref rw, rows);
                for (; rw < rows.Count; rw++)
                {
                    var row = rows[rw];
                    var cols = row.SelectNodes("./td");
                    if (cols == null) continue;
                    var col = cols[0];
                    string? txt = col.InnerText;
                    if (txt == null || !txt.Contains("follow")) continue;
                    else
                    {
                        rw++;
                        mode = 0;
                        break;
                    }
                }
            }
            else Global = new List<Lookup>();
            List<List<OneClass>> ovl = new();
            List<List<Lookup>> Locals = new();
            for (; rw < rows.Count; rw++)
            {
                var row = rows[rw];
                var cols = row.SelectNodes("./td");
                if (cols == null) continue;
                //int i = 0;
                int colspan = 0;
                bool over = false;
                for (int i = 0; i < cols.Count; i++)
                {
                    var col = cols[i];
                    string? colss = col.Attributes["colspan"]?.Value;
                    if (colss == null) colss = "1";
                    string text_col = "";
                    if (col.InnerText != null) text_col = (col.InnerText);
                    Regex regex = new Regex(@"\s+");
                    text_col = regex.Replace(text_col, " ").Trim();
                    if (mode == 0)
                    {
                        if (i == 0 && (text_col.Contains("Engineering") || text_col.Contains("Technology") || text_col.Contains("Processing") || text_col.Contains("Systems") || text_col.Contains("Chemistry") || text_col.Contains("Physics")))
                        {
                            mode = 1; break;
                        }
                        else if (text_col.Contains("Electives"))
                        {
                            over = true; break;
                        }
                        else break;
                    }
                    else if (mode == 1)
                    {
                        if (i == 0 && text_col != "&nbsp;") break;
                        else
                        {
                            var li = LocSched(ref rw, rows);
                            ovl.Add(li);
                            mode = 2;
                            if (ovl.Count == 2)
                            {
                                foreach(var lllll in ovl[1])
                                {
                                    Console.WriteLine(lllll.SlotName);
                                }
                            }
                            //rw--;
                        }
                    }
                    else if (mode == 2)
                    {
                        if (i == 0 && text_col != "Slot") break;
                        else if (i == 0 && text_col == "Slot")
                        {
                            rw++;
                            var li = BuildLookUp(ref rw, rows);
                            Locals.Add(li);
                            rw--;
                            mode = 0;
                        }
                    }
                }
                if (over) break;
            }
            List<Lookup> OE = new();
            if (Ic)
            {
                for (; rw < rows.Count; rw++)
                {
                    var row = rows[rw];
                    var cols = row.SelectNodes("./td");
                    if (cols == null) continue;
                    int i = 0;
                    int colspan = 0;
                    foreach (var col in cols)
                    {
                        string? colss = col.Attributes["colspan"]?.Value;
                        if (colss == null) colss = "1";
                        string text_col = "";
                        if (col.InnerText != null) text_col = (col.InnerText);
                        Regex regex = new Regex(@"\s+");
                        text_col = regex.Replace(text_col, " ").Trim();

                        if (text_col == "Slot")
                        {
                            rw++;
                            OE = BuildLookUp(ref rw, rows);
                            break;
                        }
                    }
                }
            }
            AddDB(Global, OE, Locals, ovl);
        }
        public static void readSchedule()
        {
            MyContext context= new MyContext();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"G:\DBTemp\attachments\2nd.htm");
            var tables = doc.DocumentNode.SelectNodes("//table");            
            if (tables == null) return;
            if (tables.Count == 0) return;
            var table = tables.FirstOrDefault();
            var rows = table.SelectNodes("./tr");
            List<Slots> slots = new List<Slots>();
            string Week = "";
            List<OneClass> liss = new List<OneClass>();
            foreach ( var row in rows )
            {
                bool hdrRow = false;
                var cols = row.SelectNodes("./td");
                if (cols == null) continue;
                int i = 0;
                int colspan = 0;
                foreach(var col in cols)
                {
                    string? colss = col.Attributes["colspan"]?.Value;
                    if (colss == null) colss = "1";
                    string text_col = "";
                    if (col.InnerText != null) text_col = (col.InnerText);
                    if (text_col.Length == 0 && (modee == 2 || modee == 4) && !hdrRow && i == 0)
                    {
                        if (modee == 4)
                        {
                            string[] star = { "MON", "TUE", "WED", "THU", "FRI" };
                            foreach (string s in star)
                            {
                                Console.WriteLine("For " + s + ":");
                                //foreach (var zzz in liss.Where(t => t.day == s))
                                //{
                                //    Console.WriteLine("\t" + zzz.SlotName + " " + zzz.TimeBegin.TotalHours + ":" + zzz.TimeBegin.TotalMinutes % 60 + " " + zzz.TimeEnd.TotalHours + ":" + zzz.TimeBegin.TotalMinutes % 60);
                                //}
                            }
                        }
                        modee = (modee + 1) % 5;
                        break;
                    }
                    Regex regex = new Regex(@"\s+");
                    text_col = regex.Replace(text_col, " ").Trim();
                    //Console.WriteLine(text_col);
                    if (hdrRow)
                    {
                        if (text_col.Length == 0) break;
                        var z = text_col.Trim().Split('-');
                        var z0 = z[0].Split('.');
                        var z1 = z[1].Split('.');
                        int z00 = Convert.ToInt32(z0[0]);                        
                        int z01 = (z0.Length == 2) ? Convert.ToInt32(z0[1]) : 0;                    
                        if (z01 >= 60)
                        {
                            z01 %= 60;
                            z00++;                            
                        }
                        int z10 = Convert.ToInt32(z1[0]);
                        int z11 = Convert.ToInt32(z1[1]);
                        z11 += 10;
                        if (z11 >= 60)
                        {
                            z11 %= 60;
                            z10++;
                        }
                        Slots slt = new Slots();
                        slt.Begin[0] = z00;
                        slt.Begin[1] = z01;
                        slt.End[0] = z10;
                        slt.End[1] = z11;
                        slt.begspan = colspan;
                        slt.endspan = colspan + Convert.ToInt32(colss);
                        slots.Add(slt);
                    }
                    if (!hdrRow && modee == 2)
                    {
                        if (i == 0)
                        {
                            Week = text_col;
                            //Console.WriteLine(Week);
                        }
                        else if (text_col == "")
                        {
                            break;
                        }
                        else
                        {
                            //if (text_col == "I")
                            //{
                            //    Console.WriteLine();
                            //}
                            if (text_col == "&nbsp;")
                            {                                
                                colspan += Convert.ToInt32(colss);
                                continue;
                            }
                            OneClass cl = new OneClass();
                            cl.span = Convert.ToInt32(colss);
                            int collll = Convert.ToInt32(colss) + colspan;
                            Slots? beg = slots.Where(x => x.begspan <= colspan && x.endspan > colspan).FirstOrDefault();
                            if (beg == null) { Console.WriteLine("ERROR"); return; }
                            Slots? end = slots.Where(x => x.begspan < collll && x.endspan >= collll).FirstOrDefault();
                            if (colspan != beg.begspan || collll != end.endspan)
                            {
                                continue;
                            }
                            cl.TimeBegin = new TimeSpan(beg.Begin[0], beg.Begin[1], 0);
                            cl.TimeEnd = new TimeSpan(end.End[0], end.End[1], 0);
                            //cl.day = Week;
                            cl.SlotName = text_col;
                            liss.Add(cl);
                        }
                    }
                    if (text_col == "&nbsp;" && modee == 1 && i == 0)
                    {                        
                        slots.Clear();
                        liss.Clear();
                        hdrRow = true;
                        modee = 2;
                    }
                    else if (text_col == "Slot" && modee == 3&& i == 0)
                    {
                        modee = 4;
                        continue;
                    }
                    else if (modee == 0 && text_col.Contains("Room No."))
                    {
                        modee++;
                        var z = text_col.Split(':').ToArray();
                        text_col = z[1].Trim();
                        int capacity = -1;
                        if (text_col.Contains('('))
                        {
                            var zz = text_col.Split('(');
                            text_col= zz[0].Trim();
                            string cap = zz[1].Trim().Replace(")", "");
                            capacity = Convert.ToInt32(cap);
                        }
                        if (!context.Rooms.Any(x => x.room_code == text_col))
                        {
                            Room rm = new Room();
                            rm.room_code = text_col;
                            rm.building = " ";
                            rm.type = " ";
                            rm.capacity = capacity;
                            context.Rooms.Add(rm);
                            context.SaveChanges();
                        }
                        Console.WriteLine(text_col);
                        continue;
                    }
                    colspan += Convert.ToInt32(colss);
                    i++;
                }
            }
        }
    }
}
