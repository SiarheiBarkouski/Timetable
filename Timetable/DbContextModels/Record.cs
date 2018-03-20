using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class Record
    {
        [Key]
        public int IdRecord { get; set; }
        public int WeekNumber { get; set; }
        public int WeekDay { get; set; }
        public int SubjOrdinalNumber { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public int IdGroup { get; set; }
        public int IdSubject { get; set; }
        public int IdLecturer { get; set; }
        public int IdSubjectType { get; set; }
        public int IdSubjectFor { get; set; }
        public int IdClassroom { get; set; }

        public Classroom Classroom { get; set; }
        public Group Group { get; set; }
        public Lecturer Lecturer { get; set; }
        public SubjectFor SubjectFor { get; set; }
        public Subject Subject { get; set; }
        public SubjectType SubjectType { get; set; }
        public List<Cancellation> Cancellation { get; set; }
    }
}
