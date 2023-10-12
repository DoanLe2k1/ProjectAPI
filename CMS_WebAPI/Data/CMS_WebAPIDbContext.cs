using CMS_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_WebAPI.Data
{
    public class CMS_WebAPIDbContext : DbContext
    {
        public CMS_WebAPIDbContext(DbContextOptions<CMS_WebAPIDbContext> options) : base(options)
        {

        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<SubjectScoreType> SubjectScoreTypes { get; set; }
        public DbSet<ScoreType> ScoreTypes { get; set; }
        public DbSet<HolidaySchedule> HolidaySchedules { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<Account>()
                .HasKey(p => p.Email);
            modelBuilder.Entity<Account>()
                .Property(p => p.Email)
                .ValueGeneratedNever();*/
            modelBuilder.Entity<Course>()
                .HasKey(p => p.CourseId);
            modelBuilder.Entity<Course>()
                .Property(p => p.CourseId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Department>()
                .HasKey(p => p.DepartmentId);
            modelBuilder.Entity<Department>()
                .Property(p => p.DepartmentId)
                .ValueGeneratedNever();
            modelBuilder.Entity<StudentScore>()
                .HasKey(p => p.StudentId);
            modelBuilder.Entity<StudentScore>()
                .Property(p => p.StudentId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Subject>()
                .HasKey(p => p.SubjectId);
            modelBuilder.Entity<Subject>()
                .Property(p => p.SubjectId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Transcript>()
                .HasKey(p => p.TranscriptId);
            modelBuilder.Entity<Transcript>()
                .Property(p => p.TranscriptId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Classroom>()
                .HasKey(p => p.ClassroomId);
            modelBuilder.Entity<Classroom>()
                .Property(p => p.ClassroomId)
                .ValueGeneratedNever();
            modelBuilder.Entity<SubjectScoreType>()
              .HasKey(p => p.SubjectScoreTypeId);
            modelBuilder.Entity<SubjectScoreType>()
                .Property(p => p.SubjectScoreTypeId)
                .ValueGeneratedNever();
            modelBuilder.Entity<HolidaySchedule>()
              .HasKey(p => p.HolidayId);
            modelBuilder.Entity<HolidaySchedule>()
                .Property(p => p.HolidayId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Revenue>()
             .HasKey(p => p.RevenueId);
            modelBuilder.Entity<Revenue>()
                .Property(p => p.RevenueId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Student>()
              .HasKey(p => p.StudentId);
            modelBuilder.Entity<Student>()
                    .Property(p => p.StudentId)
                    .ValueGeneratedNever();
            modelBuilder.Entity<Teacher>()
              .HasKey(p => p.TeacherId);
            modelBuilder.Entity<Teacher>()
                    .Property(p => p.TeacherId)
                    .ValueGeneratedNever();

        }
    }
}
