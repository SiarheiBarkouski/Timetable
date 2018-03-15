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
    }
}