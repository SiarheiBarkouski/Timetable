using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timetable.DbContextModels
{
    public class Flow
    {
        [Key]
        public int IdFlow { get; set; }
        public string Name { get; set; }
        public List<Group> Group { get; set; }
    }
}