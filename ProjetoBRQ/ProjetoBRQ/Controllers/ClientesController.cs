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

    
    public class ClientesController : Controller
    {

        private DbBRQ Db = new DbBRQ();

        [Authorize(Roles = "ADMIN")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Details(int? Id)
        {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }

            var model = await Db.Cliente.FindAsync(Id);

            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
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
                var cliente = Db.Cliente.Where(x => x.Id == Id).FirstOrDefault();
                var cb = new ClienteBusiness();
                result = await cb.DeleteAsync(Id ?? 0);

                TempData["MsgDelete"] = "O Cliente '" + cliente.Nome + "' foi removido";

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

        [Authorize(Roles = "ADMIN")]
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

        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            var model = await Db.Cliente.FindAsync(Id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
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