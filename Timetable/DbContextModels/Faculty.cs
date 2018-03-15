using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timetable.DbContextModels
{
    public class Faculty
    {
        [Key]
        public int IdFaculty { get; set; }
        public string NameFaculty { get; set; }

        public virtual List<Group> Group { get; set; }
    }
}