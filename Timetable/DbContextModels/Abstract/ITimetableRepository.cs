using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.DbContextModels.Abstract
{
    public interface ITimetableRepository
    {
        IEnumerable<Faculty> Faculties { get; }
        IEnumerable<Flow> Flows { get; }
        IEnumerable<Group> Groups { get; }
        IEnumerable<Chair> Chairs { get; }
        IEnumerable<Classroom> Classrooms { get; }
        IEnumerable<Lecturer> Lecturers { get; }
        IEnumerable<SubjectFor> SubjectFors { get; }
        IEnumerable<Subject> Subjects { get; }
        IEnumerable<SubjectType> SubjectTypes { get; }
        IEnumerable<Cancellation> Cancellations { get; }
        IEnumerable<Record> Records { get; }
    }
}