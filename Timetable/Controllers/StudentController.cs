using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timetable.DbContextModels;
using Timetable.DbContextModels.Abstract;
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
    }
}