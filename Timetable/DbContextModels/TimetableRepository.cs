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
        public IEnumerable<Chair> Chairs => _context.Chairs;
        public IEnumerable<Classroom> Classrooms => _context.Classrooms;
        public IEnumerable<Lecturer> Lecturers => _context.Lecturers;
        public IEnumerable<SubjectFor> SubjectFors => _context.SubjectFors;
        public IEnumerable<Subject> Subjects => _context.Subjects;
        public IEnumerable<SubjectType> SubjectTypes => _context.SubjectTypes;
        public IEnumerable<Cancellation> Cancellations => _context.Cancellations;
        public IEnumerable<Record> Records => _context.Records;
    }
}