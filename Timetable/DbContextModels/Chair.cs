using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class Chair
    {
        [Key]
        public int IdChair { get; set; }
        public string NameChair { get; set; }

        public List<Lecturer> Lecturer { get; set; }
        public List<Subject> Subject { get; set; }
    }
}
