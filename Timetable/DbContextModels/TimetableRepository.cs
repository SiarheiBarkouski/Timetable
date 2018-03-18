using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timetable.DbContextModels.Abstract;

namespace Timetable.DbContextModels
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly TimetableContext _context = new TimetableContext();

        public IEnumerable<Faculty> Faculties => _context.Faculties;
        public IEnumerable<Flow> Flows => _context.Flows;
        public IEnumerable<Group> Groups => _context.Groups;
    }
}