using System;
using System.Linq;
using System.Web.Mvc;
using ProjetoBRQ.Models;
using ProjetoBRQ.Business;
using System.Threading.Tasks;
using ProjetoBRQ.Context;

namespace ProjetoBRQ.Controllers
{
    //[Authorize]
    public class InvestimentController : Controller
    {
        private DbBRQ Db = new DbBRQ();

        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                    return RedirectToAction("Index");

                var investiment = await Db.Investiment.FindAsync(id);

                if(investiment == null)
                {
                    ModelState.AddModelError("", "Investimento não encontrado");
                    return RedirectToAction("Index");
                }

                return View(investiment);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("Index");
            }
            
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Investiment model)
        {
            string result = "";
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new InvestmentBusiness();
                result =  await cb.AddAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return View(model);
                }
                return RedirectToAction("Details", new { Id = model.Id});
            }
            catch(Exception e)
            {
                ModelState.AddModelError("",e.Message);
                return View(model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var investimento = await Db.Investiment.FindAsync(id);

            if(investimento == null)
            {
                ModelState.AddModelError("", "Investimento não encontrado");
                return RedirectToAction("Index");
            }

            return View(investimento);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(Investiment model)
        {
            string result = "";
            try
            {
                var cb =  new InvestmentBusiness();

                result = await cb.UpdateAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Details", new { Id = model.Id });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await new InvestmentBusiness().DeleteAsync(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return RedirectToAction("Index");
        }

        public JsonResult TableIndex(int? Page, Investiment model)
        {
            const int registers = 10;
            IQueryable<Investiment> query;
            Page = Page ?? 1;
            Page--;

            query = Db.Investiment.Where(x => !x.Deleted);

            if (model.Id != null)
                query = query.Where(m => m.Id == model.Id);
            if (model.Name != null)
                query = query.Where(m => m.Name == model.Name);
            if (model.Stock != null)
                query = query.Where(m => m.Stock == model.Stock);
            if (model.Value != null)
                query = query.Where(m => m.Value == model.Value);

            var arr = query.OrderByDescending(x => x.Id).Skip(Page.Value * registers).Take(registers).ToArray();
            var count = query.Count();
            int countPages = (int)(count / registers);

            return Json(new
            {
                list = arr,
                count = count,
                countPages = countPages,
                cod = model.Id,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
