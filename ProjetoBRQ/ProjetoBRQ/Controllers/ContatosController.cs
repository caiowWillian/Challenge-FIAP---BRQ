using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBRQ.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ContatosController : Controller
    {
        private DbBRQ Db = new DbBRQ();

        // GET: Contatos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Contatos model)
        {
            string result = "";
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new ContatosBusiness();
                result = await cb.AddAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return View(model);
                }
            }catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
            return RedirectToAction("Details", new { id = Convert.ToInt32(result) });
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var model = await Db.Contatos.FindAsync(id);

            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Contatos model)
        {
            string result = "";
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new ContatosBusiness();
                result = await cb.UpdateAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return View(model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
            return RedirectToAction("Details", new { id = Convert.ToInt32(result) });
        }

        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var model = await Db.Contatos.FindAsync(id);

            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            if(await Db.Contatos.FindAsync(id) == null)
            {
                return RedirectToAction("Index");
            }

            string result = "";

            try
            {
                var cb = new ContatosBusiness();
                result = await cb.DeleteAsync(id ?? 0);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Erro ao deletar: " + e.Message);
            }
            return RedirectToAction("Index");
        }

        public JsonResult TableIndex(int? Page, Contatos model)
        {
            const int registers = 10;

            Page = Page ?? 1;
            Page--;

            IQueryable<Contatos> query = Db.Contatos.Where(x => !x.Deletado);

            if (model.Id != null)
                query = query.Where(m => m.Id == model.Id);
            if (!string.IsNullOrEmpty(model.Nome))
                query = query.Where(m => m.Nome == model.Nome);
            if (!string.IsNullOrEmpty(model.Email))
                query = query.Where(m => m.Email == model.Email);
            if (!string.IsNullOrEmpty(model.Telefone))
                query = query.Where(m => m.Telefone == model.Telefone);

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