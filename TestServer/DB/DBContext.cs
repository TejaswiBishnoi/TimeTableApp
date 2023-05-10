using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    public class MyContext : DbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Occurence> Occurences { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Instructor_Of> Instrucor_Ofs { get; set; }
        public DbSet<Teaches> Teaches_ { get; set; }
        public DbSet<Calendar> Calendar { get; set; }

        public DbSet<GoogleMiddleToken> GoogleMiddleTokens { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>().HasKey(s=>s.instructor_id);
            modelBuilder.Entity<Course>().HasKey(s => s.course_code);
            modelBuilder.Entity<Section>().HasKey(s => s.section_id);
            modelBuilder.Entity<Event>().HasKey(s => s.event_id);
            modelBuilder.Entity<Occurence>().HasKey(s => new {s.occurence_id, s.event_id});
            modelBuilder.Entity<Room>().HasKey(s => s.room_code);
            modelBuilder.Entity<Instructor_Of>().HasKey(s => new {s.course_code, s.instrucor_id});
            modelBuilder.Entity<Teaches>().HasKey(s => new {s.section_id, s.instructor_id});
            modelBuilder.Entity<Calendar>().HasKey(s => new { s.instructor_id });
            modelBuilder.Entity<GoogleMiddleToken>().HasKey(s => s.token);
            //modelBuilder.Entity<GoogleToken>().HasKey(s => new { s.instructor_id });

            modelBuilder.Entity<Instructor>().Property(s=>s.email_id).IsRequired();
            modelBuilder.Entity<Instructor>().Property(s => s.name).IsRequired();
            modelBuilder.Entity<Instructor>().HasIndex(s => s.email_id).IsUnique();

            modelBuilder.Entity<Course>().Property(s => s.total_credits).HasColumnType("decimal(2,1)").HasDefaultValue(0.0).IsRequired();
            modelBuilder.Entity<Course>().Property(s => s.lecture_credits).HasColumnType("decimal(2,1)").HasDefaultValue(0.0).IsRequired();
            modelBuilder.Entity<Course>().Property(s => s.tutorial_credits).HasColumnType("decimal(2,1)").HasDefaultValue(0.0).IsRequired();
            modelBuilder.Entity<Course>().Property(s => s.practical_credits).HasColumnType("decimal(2,1)").HasDefaultValue(0.0).IsRequired();
            modelBuilder.Entity<Course>().Property(s => s.coordinator_id).HasDefaultValue("ProfX").IsRequired();

            modelBuilder.Entity<Section>().Property(s => s.course_code).IsRequired();
            modelBuilder.Entity<Section>().Property(s => s.event_id).IsRequired();
            modelBuilder.Entity<Section>().Property(s => s.name).IsRequired().HasDefaultValue("All");

            modelBuilder.Entity<Event>().Property(s =>s.event_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Event>().Property(s => s.name).IsRequired();            
            modelBuilder.Entity<Event>().Property(s => s.is_course).IsRequired().HasDefaultValue(false);            
            modelBuilder.Entity<Event>().Property(s => s.ignore_holiday).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<Event>().Property(s => s.owner).IsRequired().HasDefaultValue("ProfX");

            modelBuilder.Entity<Occurence>().Property(s => s.occurence_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Occurence>().Property(s => s.event_id).IsRequired();
            modelBuilder.Entity<Occurence>().Property(s => s.time_begin).IsRequired().HasColumnType("time");
            modelBuilder.Entity<Occurence>().Property(s => s.time_end).IsRequired().HasColumnType("time");
            modelBuilder.Entity<Occurence>().Property(s => s.day).IsRequired();
            modelBuilder.Entity<Occurence>().HasCheckConstraint("Day_Cons", "day < 7 AND day > -1");
            modelBuilder.Entity<Occurence>().Property(s => s.date_start).HasColumnType("date").IsRequired();
            modelBuilder.Entity<Occurence>().Property(s => s.date_end).HasColumnType("date").IsRequired();

            modelBuilder.Entity<Room>().Property(s => s.capacity).IsRequired();

            modelBuilder.Entity<Instructor_Of>().Property(s => s.instrucor_id).IsRequired();
            modelBuilder.Entity<Instructor_Of>().Property(s => s.course_code).IsRequired();

            modelBuilder.Entity<Teaches>().Property(s => s.instructor_id).IsRequired();
            modelBuilder.Entity<Teaches>().Property(s => s.section_id).IsRequired();

            modelBuilder.Entity<Course>().HasOne<Instructor>(s => s.coordinator).WithMany(g => g.courses).HasForeignKey(s => s.coordinator_id);

            modelBuilder.Entity<Instructor_Of>().HasOne<Instructor>(s => s.instructor).WithMany(g => g.instrucor_of).HasForeignKey(s => s.instrucor_id);
            modelBuilder.Entity<Instructor_Of>().HasOne<Course>(s => s.course).WithMany(g => g.instrucor_of).HasForeignKey(s => s.course_code);

            modelBuilder.Entity<Teaches>().HasOne<Instructor>(s => s.instructor).WithMany(g => g.teaches).HasForeignKey(s => s.instructor_id);
            modelBuilder.Entity<Teaches>().HasOne<Section>(s => s.section).WithMany(g => g.teaches).HasForeignKey(s => s.section_id);

            modelBuilder.Entity<Section>().HasOne<Course>(s => s.course).WithMany(g => g.sections).HasForeignKey(s => s.course_code);
            modelBuilder.Entity<Section>().HasOne<Event>(s => s.Event).WithOne(g => g.section).HasForeignKey<Section>(s => s.event_id);

            modelBuilder.Entity<Event>().HasOne<Instructor>(s => s.Owner).WithMany(g => g.events).HasForeignKey(s => s.owner);

            modelBuilder.Entity<Occurence>().HasOne<Event>(s => s.event_).WithMany(g => g.occurences).HasForeignKey(s => s.event_id);
            modelBuilder.Entity<Occurence>().HasOne<Room>(s => s.room).WithMany(g => g.occurences).HasForeignKey(s => s.room_code);

            modelBuilder.Entity<Calendar>().HasOne<Instructor>(s => s.instructor).WithOne(s => s.calendar).HasForeignKey<Calendar>(s => s.instructor_id);
            modelBuilder.Entity<GoogleMiddleToken>().HasOne<Instructor>(s => s.instructor).WithOne(s => s.googletoken).HasForeignKey<GoogleMiddleToken>(s => s.instructor_id);
        }
    }
}
