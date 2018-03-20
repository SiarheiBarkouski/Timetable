using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class SubjectFor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Record> Record { get; set; }
    }
}
