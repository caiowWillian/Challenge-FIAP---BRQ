using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjetoBRQ.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class NewsController : Controller
    {
        private DbBRQ Db = new DbBRQ();

        public ActionResult Index()
        {
            var user = User.Identity;
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var s = UserManager.GetRoles(user.GetUserId());
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(News model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int id = await new NewsBusiness().AddAsync(model);

            if (id > 0)
            {
                if (model.File != null)
                {
                    try
                    {
                        int idImg = await new ImgNewsBusiness().AddAsync(model.File, id);
                    }
                    catch (Exception) { }
                }
            }
            if (id < 0)
            {
                return View(model);
            }
            return RedirectToAction("Details", new { Id = id });
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
                var q = u.ImgNews;
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
        public async Task<ActionResult> Edit(News model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (model.File != null)
                {
                    //Update
                    var img = Db.ImgNews.Where(x => x.IdNews == model.Id).FirstOrDefault();
                    if (img != null)
                    {
                        await new ImgNewsBusiness().UpdateAsync(model.File, img.Id);
                    }
                    else //Insert
                    {
                        await new ImgNewsBusiness().AddAsync(model.File, model.Id ?? 0);
                    }

                }
                await new NewsBusiness().UpdateAsync(model);
            }
            catch (Exception) { return View("Index"); }

            return RedirectToAction("Details",new { Id = model.Id });
        }

        public async Task<ActionResult> Delete(int? Id)
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

                await new NewsBusiness().DeleteAsync(Id.Value);
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

        public JsonResult TableIndex(int? Page, News Model)
        {
            const int registers = 10;
            
            Page = Page ?? 1;
            Page--;

            var query = Db.News.Where(x => x.Deletado != 1);

            if (Model.Id != null)
                query = query.Where(x => x.Id == Model.Id);

            if (Model.Title != null)
                query = query.Where(x => x.Title == Model.Title);

            //var arr = Db.News.Where(x => x.Deletado != 1).OrderByDescending(x => x.Id).Skip(Page.Value* registers).Take(registers).ToArray();

            var arr = query.OrderByDescending(x => x.Id).Skip(Page.Value * registers).Take(registers).ToArray();
            var count = query.Count();
            int countPages = (int)(count / registers);

            return Json(new
            {
                list = arr,
                count = count,
                countPages = countPages,
                cod = Model.Id,
                titulo = Model.Title == null ? "" : Model.Title
            }, JsonRequestBehavior.AllowGet);
        }
    }
}