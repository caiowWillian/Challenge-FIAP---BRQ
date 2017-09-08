using Newtonsoft.Json;
using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(News News)
        {
            if (ModelState.IsValid)
            {
                int id = new NewsBusiness().Add(News);
                if(id > 0)
                {
                    if(News.File != null)
                    {
                        int idImg = new ImgNewsBusiness().Add(News.File, id);
                    }
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
            Db.ImgNews.Where(x => x.IdNews == Id).ToList();
            if (u == null)
            {
                return View("Index");
            }

            return View(u);
        }

        public ActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var n = Db.News.Where(x => x.Id == Id).FirstOrDefault();
            Db.ImgNews.Where(x => x.IdNews == Id).ToList();

            if(n == null)
            {
                return RedirectToAction("Index");
            }

            return View(n);
        }

        [HttpPost]
        public ActionResult Edit(News News)
        {
            if(News.File != null)
            {
                //Update
                var img = Db.ImgNews.Where(x => x.IdNews == News.Id).FirstOrDefault();
                if (img != null)
                {
                    new ImgNewsBusiness().Update(News.File,img.Id);
                }
                else //Insert
                {
                    new ImgNewsBusiness().Add(News.File, News.Id);
                }

            }
            new NewsBusiness().Update(News);

            return RedirectToAction("Details",new { Id = News.Id });
        }

        public JsonResult TableIndex()
        {
            var content = new StringContent(JsonConvert.SerializeObject(Db.News.ToList()), Encoding.UTF8, "application/json").ToString();

            var arr = Db.News.ToArray();

            return Json(new { list = arr },JsonRequestBehavior.AllowGet);
        }
    }
}