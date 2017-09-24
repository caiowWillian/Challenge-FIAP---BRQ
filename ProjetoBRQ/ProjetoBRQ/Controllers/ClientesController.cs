using ProjetoBRQ.Business;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using ProjetoBRQ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBRQ.Controllers
{

    [Authorize(Roles = "ADMIN")]
    public class ClientesController : Controller
    {

        private DbBRQ Db = new DbBRQ();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var model = Db.Cliente.Find(Id);

            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int? Id)
        {
            if(Id == null)
            {
                ModelState.AddModelError("", "Código invalido!");
                return RedirectToAction("Index");
            }
            
            if(await Db.Cliente.FindAsync(Id) == null)
            {
                ModelState.AddModelError("", "Cliente não encontrado!");
                return RedirectToAction("Index");
            }

            string result = "";
            try
            {
                var cb = new ClienteBusiness();
                result = await cb.DeleteAsync(Id ?? 0);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Erro ao deletar "+e.Message);
            }
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cliente model)
        {
            string result = "";
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new ClienteBusiness();
                result = await cb.AddAsync(model);

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

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var model = Db.Cliente.Find(Id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Cliente model)
        {
            string result = "";
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new ClienteBusiness();
                result = await cb.UpdateAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return View(model);
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        public JsonResult TableIndex(int? Page, Cliente model)
        {
            const int registers = 10;

            Page = Page ?? 1;
            Page--;

            IQueryable<Cliente> query = Db.Cliente.Where(x => !x.Deletado);

            if (model.Id != null)
                query = query.Where(m => m.Id == model.Id);

            if (model.Cpf != null)
                query = query.Where(m => m.Cpf == model.Cpf);

            if (model.Nome != null)
                query = query.Where(m => m.Nome == model.Nome);

            if (model.Email != null)
                query = query.Where(m => m.Email == model.Email);

            var arr = query.OrderByDescending(x => x.Id).Skip(Page.Value * registers).Take(registers).ToArray();
            var count = Db.Cliente.Count();
            int countPages = (int)(count / registers);

            return Json(new
            {
                list = arr,
                count = count,
                countPages = countPages
            }, JsonRequestBehavior.AllowGet);
        }
    }
}