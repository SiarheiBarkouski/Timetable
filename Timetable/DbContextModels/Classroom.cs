using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class Classroom
    {
        [Key]
        public int IdClassroom { get; set; }
        public string Name { get; set; }
        public int Building { get; set; }

        public List<Record> Record { get; set; }
    }
}
