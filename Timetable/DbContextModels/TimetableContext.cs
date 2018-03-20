using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Timetable.DbContextModels
{
    public class TimetableContext : DbContext
    {
        public TimetableContext() : base("TimetableConnection")
        {
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<SubjectFor> SubjectFors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}