﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timetable.Models
{
    public class StudentRecordViewModel
    {
        public int IdRecord { get; set; }
        public int WeekDay { get; set; }
        public int WeekNumber { get; set; }
        public string LectureName { get; set; }
        public string SubjectName { get; set; }
        public int SubjOrdinalNumber { get; set; }
        public string Classroom { get; set; }
        public int IdSubjectType { get; set; }
    }
}