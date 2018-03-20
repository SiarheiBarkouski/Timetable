using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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

        public FileResult DownloadDeals(string dateFrom, string dateTo, int instrumentId)
        {
            DateTime dtFrom = DateTime.Now.Date;
            DateTime dtTo = DateTime.Now.Date;
            if (dateFrom != null && dateTo != null)
            {
                dtFrom = DateTime.ParseExact(dateFrom, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                dtTo = DateTime.ParseExact(dateTo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            dtTo = dtTo.AddDays(1);
            tryDeleteOldImages();

            var fileName = "Timetable_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ".xlsx";
            var outputDir = HostingEnvironment.MapPath("~/Content/DealsExcels/");

            var file = new FileInfo(outputDir + fileName);
            using (ExcelPackage xlPackage = new ExcelPackage(file))
            {
                xlPackage.Workbook.Worksheets.Add("Расписание");
                var worksheet = xlPackage.Workbook.Worksheets[1];
                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;
                worksheet.Cells["A1:E1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Value = "Инструмент";
                worksheet.Cells["B1"].Value = "Дата сделки";
                worksheet.Cells["C1"].Value = "Курс";
                worksheet.Cells["D1"].Value = "Лот";
                worksheet.Cells["E1"].Value = "Объем";

                using (var context = new bcse_webEntities())
                {

                    var listOfDeals =
                    (
                        from instrument in context.Instruments
                        join deal in context.BcseOnlineDeals on instrument.Id equals deal.InstrumentId
                        where (deal.DealDate > dtFrom && deal.DealDate < dtTo) && deal.InstrumentId == instrumentId
                        orderby deal.DealDate descending
                        select new { instrumentName = instrument.Name, deal.DealDate, deal.Course, deal.LotAmount, deal.AttAmount, deal.InstrumentId }
                    ).ToList();

                    int i = 2;
                    foreach (var d in listOfDeals)
                    {
                        worksheet.Cells["A" + i].Value = d.instrumentName;
                        worksheet.Cells["B" + i].Value = d.DealDate.ToString();
                        worksheet.Cells["C" + i].Value = d.Course;
                        worksheet.Cells["D" + i].Value = d.LotAmount;
                        worksheet.Cells["E" + i++].Value = d.AttAmount;
                    }

                }
                xlPackage.Save();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(HostingEnvironment.MapPath("~/Content/DealsExcels/" + fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private void tryDeleteOldImages()
        {
            string[] files = null;
            try
            {
                files = Directory.GetFiles(HostingEnvironment.MapPath("~/Content/DealsExcels/"));
            }
            catch
            {
                Directory.CreateDirectory(HostingEnvironment.MapPath("~/Content/DealsExcels/"));
                files = Directory.GetFiles(HostingEnvironment.MapPath("~/Content/DealsExcels/"));
            }
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (DateTime.Now.Subtract(fi.CreationTime).Minutes > 5)
                    fi.Delete();
            }
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