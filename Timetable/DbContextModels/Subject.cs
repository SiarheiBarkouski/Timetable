using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class Subject
    {
        [Key]
        public int IdSubject { get; set; }
        public string NameSubject { get; set; }
        public int IdChair { get; set; }
        public int EduLevel { get; set; }
        public string AbnameSubject { get; set; }

        public Chair Chair { get; set; }
        public List<Record> Record { get; set; }
    }
}
