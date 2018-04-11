using RSSHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test_task.Models;
namespace test_task.Controllers
{
    public class HomeController : Controller
    {
        private SampleContext _db;
        public HomeController()
        {
            _db = new SampleContext();
        }
        public ActionResult Index()
        {
            var result = _db.Sources.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult ReadNews(int id)
        {
            var result = _db.News.Where(x => x.Source.Id == id);
            SelectList sources = new SelectList(_db.Sources, "Id", "Name");
            ViewBag.Sources = sources;
            return View(result);
        }

        [HttpPost]
        public ActionResult ReadNews(FormModel model)
        {
            var result = _db.News.Where(x => x.Source.Id == model.SourceId);
            if (model.Sort == "date")
            {
                result = result.OrderByDescending(x => x.PublicationDate);
            }
            else
            {
                result = result.OrderBy(x => x.Title);
            }

            return Json(result.Select(x => new { Title = x.Title, Description = x.Description, PublicationDate = x.PublicationDate, SourceName = x.Source.Name}).ToList());
        }
    }
}