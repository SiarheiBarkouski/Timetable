using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.DbContextModels.Abstract;
using Timetable.Models;
using Timetable.Services;

namespace Timetable.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ITimetableRepository _repository;

        public LecturerController(ITimetableRepository repository)
        {
            _repository = repository;
        }
        
        public ActionResult Index(string searchString, int? page)
        {
            ViewData["searchString"] = searchString;
            var lecturers = (from p in _repository.Lecturers
                             select new LectureViewModel { IdLecturer = p.IdLecturer, NameLecturer = p.NameLecturer });

            if (!String.IsNullOrEmpty(searchString))
            {
                lecturers = lecturers.Where(g => g.NameLecturer.Contains(searchString));
            }

            return View(PaginatedList<LectureViewModel>.Create(lecturers, page ?? 1, 10));
        }

        public ActionResult DetailsWeek(int id)
        {            
            ViewData["currWeek"] = GetCurrentWeek();
            ViewData["lectureName"] = (from p in _repository.Lecturers
                                       where p.IdLecturer == id
                                       select p.NameLecturer).First();

            var records = (from r in _repository.Records
                           join l in _repository.Groups on r.IdGroup equals l.IdGroup
                           join s in _repository.Subjects on r.IdSubject equals s.IdSubject
                           join c in _repository.Classrooms on r.IdClassroom equals c.IdClassroom
                           where (r.IdLecturer == id)
                                                      
                           orderby r.WeekDay, r.SubjOrdinalNumber, r.WeekNumber
                           select new LectureRecordViewModel
                           {
                               IdRecord = r.IdRecord,
                               WeekDay = r.WeekDay,
                               WeekNumber = r.WeekNumber,
                               GroupName = l.NameGroup,
                               SubjectName = s.AbnameSubject,
                               SubjOrdinalNumber = r.SubjOrdinalNumber,
                               Classroom = c.Name + " (к." + c.Building + ")",
                               IdSubjectType = r.IdSubjectType
                           }
            ).ToList();

            return View(records);
        }
        
        public int GetCurrentWeek()
        {
            var dtNow = new DateTime();
            dtNow = DateTime.Now;
            int year;


            if (dtNow.Month < 9)
                year = dtNow.Year - 1;
            else
                year = dtNow.Year;

            var dtSept = new DateTime(year, 9, 1);

            var diff = dtNow - dtSept;
            int differenceInDays = diff.Days;
            differenceInDays += (int)dtSept.DayOfWeek - 1;
            int currentWeek = 1 + (differenceInDays % 28) / 7;
            return currentWeek;
        }

    }
}