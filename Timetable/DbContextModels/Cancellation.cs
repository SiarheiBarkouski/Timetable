using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timetable.DbContextModels
{
    public class Cancellation
    {
        [Key]
        public int IdCancellation { get; set; }
        public int IdRecord { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime DateFrom { get; set; }

        public Record Record { get; set; }
    }
}
