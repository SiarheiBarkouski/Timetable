﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.DbContextModels;
using Timetable.DbContextModels.Abstract;
using Timetable.Models;
using Timetable.Services;

namespace Timetable.Controllers
{
    public class StudentController : Controller
    {
        private readonly ITimetableRepository _repository;

        public StudentController(ITimetableRepository repository)
        {
            _repository = repository;
        }
        public ViewResult Index(string searchString, int? page)
        {
            var groups = _repository.Groups.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(g => g.NameGroup.Contains(searchString)).ToList();
            }
            return View(PaginatedList<Group>.Create(groups, page ?? 1, 10));
        }


        public ActionResult DetailsWeek(int idgroup, int subgroup)
        {
            ViewData["currWeek"] = GetCurrentWeek();
            ViewData["subgroup"] = subgroup;
            ViewData["groupName"] = (from p in _repository.Groups
                                     where p.IdGroup == idgroup
                                     select p.NameGroup).First();

            var records = (from r in _repository.Records
                           join l in _repository.Lecturers on r.IdLecturer equals l.IdLecturer
                           join s in _repository.Subjects on r.IdSubject equals s.IdSubject
                           join c in _repository.Classrooms on r.IdClassroom equals c.IdClassroom
                           where (r.IdGroup == idgroup) &&
                           new[] { subgroup, 3 }.Contains(r.IdSubjectFor)

                           //    && (r.DateTo >= DateTime.Today && r.DateFrom <= DateTime.Today)
                           orderby r.WeekDay, r.SubjOrdinalNumber, r.WeekNumber
                           select new StudentRecordViewModel
                           {
                               IdRecord = r.IdRecord,
                               WeekDay = r.WeekDay,
                               WeekNumber = r.WeekNumber,
                               LectureName = l.NameLecturer,
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