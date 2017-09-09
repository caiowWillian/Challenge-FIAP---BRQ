using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Linq;
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
                        try
                        {
                            int idImg = new ImgNewsBusiness().Add(News.File, id);
                        }
                        catch (Exception) { }
                        
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

            try
            {
                var u = Db.News.Where(x => x.Id == Id).FirstOrDefault();
                Db.ImgNews.Where(x => x.IdNews == Id).ToList();

                if (u == null)
                {
                    return View("Index");
                }
                return View(u);
            }
            catch (Exception) { return View("Index"); }

        }

        public ActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                var n = Db.News.Where(x => x.Id == Id).FirstOrDefault();
                Db.ImgNews.Where(x => x.IdNews == Id).ToList();

                if (n == null)
                {
                    return RedirectToAction("Index");
                }
                return View(n);
            }
            catch (Exception) { return View("Index"); }
        }

        [HttpPost]
        public ActionResult Edit(News News)
        {
            try
            {
                if (News.File != null)
                {
                    //Update
                    var img = Db.ImgNews.Where(x => x.IdNews == News.Id).FirstOrDefault();
                    if (img != null)
                    {
                        new ImgNewsBusiness().Update(News.File, img.Id);
                    }
                    else //Insert
                    {
                        new ImgNewsBusiness().Add(News.File, News.Id);
                    }

                }
                new NewsBusiness().Update(News);
            }
            catch (Exception) { return View("Index"); }

            return RedirectToAction("Details",new { Id = News.Id });
        }

        public ActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            try
            {

                var news = Db.News.Where(x => x.Id == Id).FirstOrDefault();

                if (news == null)
                {
                    return RedirectToAction("Index");
                }

                new NewsBusiness().Delete(Id.Value);
            }
            catch (Exception) { return View("Index"); }

            return RedirectToAction("Index");
        }

        public ActionResult Preview(int? Id)
        {
            News news = null;
            if(Id == null)
            {
                return View("Index");
            }

            try
            {
                news = Db.News.Where(x => x.Id == Id).FirstOrDefault();

                if(news == null)
                {
                    return View("Index");
                }
            }
            catch (Exception) { return View("Index"); }

            return View(news);
        }

        public JsonResult TableIndex(int? Page)
        {
            const int registers = 10;

            Page = Page ?? 1;
            Page--;

            var arr = Db.News.Where(x => x.Deletado != 1).OrderBy(x => x.Id).Skip(Page.Value* registers).Take(registers).ToArray();
            var count = Db.News.Count();
            var countPages = (count % registers) == 0 ? (count / registers) : Convert.ToInt32((count / registers)) + 1;

            return Json(new
            {
                list = arr,
                count = count,
                countPages = countPages
            }, JsonRequestBehavior.AllowGet);
        }
    }
}