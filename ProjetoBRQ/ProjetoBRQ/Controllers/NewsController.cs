using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBRQ.Controllers
{
    public class NewsController : Controller
    {
        private DbBRQ Db = new DbBRQ();

        // GET: Noticia
        public ActionResult Index()
        {
            var t = Db.News.ToList();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(News News, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                int id = new NewsBusiness().Add(News);
                if(id > 0)
                {
                    int idImg = new ImgNewsBusiness().Add(file,id);           
                }


                if(id < 0)
                {
                    return View(News);
                }
                return RedirectToAction("Details", new { Id = id });
            }

            return View();
        }

        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return View("Index");
            }

            var u = Db.News.Where(x => x.Id == Id).FirstOrDefault();

            if (u == null)
            {
                return View("Index");
            }
            return View(u);
        }
    }
}