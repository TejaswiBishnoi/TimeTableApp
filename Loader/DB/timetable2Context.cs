using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Loader.DB
{
    public partial class timetable2Context : DbContext
    {
        public timetable2Context()
        {
        }

        public timetable2Context(DbContextOptions<timetable2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<Occurence> Occurences { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=timetable2;user=dip;password=password;persist security info=False;connect timeout=300", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseCode)
                    .HasName("PRIMARY");

                entity.ToTable("courses");

                entity.HasIndex(e => e.CoordinatorId, "IX_Courses_coordinator_id");

                entity.Property(e => e.CourseCode).HasColumnName("course_code");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CoordinatorId)
                    .HasColumnName("coordinator_id")
                    .HasDefaultValueSql("'ProfX'");

                entity.Property(e => e.CourseName).HasColumnName("course_name");

                entity.Property(e => e.Department).HasColumnName("department");

                entity.Property(e => e.LectureCredits)
                    .HasPrecision(2, 1)
                    .HasColumnName("lecture_credits");

                entity.Property(e => e.PracticalCredits)
                    .HasPrecision(2, 1)
                    .HasColumnName("practical_credits");

                entity.Property(e => e.TotalCredits)
                    .HasPrecision(2, 1)
                    .HasColumnName("total_credits");

                entity.Property(e => e.TutorialCredits)
                    .HasPrecision(2, 1)
                    .HasColumnName("tutorial_credits");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Coordinator)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CoordinatorId)
                    .HasConstraintName("FK_Courses_Instructors_coordinator_id");

                entity.HasMany(d => d.Instrucors)
                    .WithMany(p => p.CourseCodes)
                    .UsingEntity<Dictionary<string, object>>(
                        "InstrucorOf",
                        l => l.HasOne<Instructor>().WithMany().HasForeignKey("InstrucorId").HasConstraintName("FK_Instrucor_Ofs_Instructors_instrucor_id"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseCode").HasConstraintName("FK_Instrucor_Ofs_Courses_course_code"),
                        j =>
                        {
                            j.HasKey("CourseCode", "InstrucorId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("instrucor_ofs");

                            j.HasIndex(new[] { "InstrucorId" }, "IX_Instrucor_Ofs_instrucor_id");

                            j.IndexerProperty<string>("CourseCode").HasColumnName("course_code");

                            j.IndexerProperty<string>("InstrucorId").HasColumnName("instrucor_id");
                        });
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events");

                entity.HasIndex(e => e.Owner, "IX_Events_owner");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.IgnoreHoliday).HasColumnName("ignore_holiday");

                entity.Property(e => e.IsCourse).HasColumnName("is_course");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasDefaultValueSql("'ProfX'");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK_Events_Instructors_owner");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("instructors");

                entity.HasIndex(e => e.EmailId, "IX_Instructors_email_id")
                    .IsUnique();

                entity.Property(e => e.InstructorId).HasColumnName("instructor_id");

                entity.Property(e => e.Department).HasColumnName("department");

                entity.Property(e => e.EmailId).HasColumnName("email_id");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Occurence>(entity =>
            {
                entity.HasKey(e => new { e.OccurenceId, e.EventId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("occurences");

                entity.HasIndex(e => e.EventId, "IX_Occurences_event_id");

                entity.HasIndex(e => e.RoomCode, "IX_Occurences_room_code");

                entity.Property(e => e.OccurenceId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("occurence_id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.DateEnd).HasColumnName("date_end");

                entity.Property(e => e.DateStart).HasColumnName("date_start");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.RoomCode).HasColumnName("room_code");

                entity.Property(e => e.TimeBegin)
                    .HasColumnType("time")
                    .HasColumnName("time_begin");

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("time")
                    .HasColumnName("time_end");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Occurences)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_Occurences_Events_event_id");

                entity.HasOne(d => d.RoomCodeNavigation)
                    .WithMany(p => p.Occurences)
                    .HasForeignKey(d => d.RoomCode)
                    .HasConstraintName("FK_Occurences_Rooms_room_code");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomCode)
                    .HasName("PRIMARY");

                entity.ToTable("rooms");

                entity.Property(e => e.RoomCode).HasColumnName("room_code");

                entity.Property(e => e.Building).HasColumnName("building");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("sections");

                entity.HasIndex(e => e.CourseCode, "IX_Sections_course_code");

                entity.HasIndex(e => e.EventId, "IX_Sections_event_id")
                    .IsUnique();

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.CourseCode).HasColumnName("course_code");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasDefaultValueSql("_utf8mb4\\'All\\'");

                entity.HasOne(d => d.CourseCodeNavigation)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.CourseCode)
                    .HasConstraintName("FK_Sections_Courses_course_code");

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.Section)
                    .HasForeignKey<Section>(d => d.EventId)
                    .HasConstraintName("FK_Sections_Events_event_id");

                entity.HasMany(d => d.Instructors)
                    .WithMany(p => p.Sections)
                    .UsingEntity<Dictionary<string, object>>(
                        "Teach",
                        l => l.HasOne<Instructor>().WithMany().HasForeignKey("InstructorId").HasConstraintName("FK_Teaches__Instructors_instructor_id"),
                        r => r.HasOne<Section>().WithMany().HasForeignKey("SectionId").HasConstraintName("FK_Teaches__Sections_section_id"),
                        j =>
                        {
                            j.HasKey("SectionId", "InstructorId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("teaches_");

                            j.HasIndex(new[] { "InstructorId" }, "IX_Teaches__instructor_id");

                            j.IndexerProperty<string>("SectionId").HasColumnName("section_id");

                            j.IndexerProperty<string>("InstructorId").HasColumnName("instructor_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
