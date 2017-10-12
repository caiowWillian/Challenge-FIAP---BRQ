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
    public class InvestimentUserController : Controller
    {
        private DbBRQ Db = new DbBRQ();

        // GET: InvestimentUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: InvestimentUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize]
        //GET: InvestimentUser/Create
        public ActionResult Create()
        {
            ViewBag.InvestimentId = new SelectList(Db.Investiment.OrderBy(x => x.Id), "Id", "Name");
            ViewBag.CurrentUser = User.Identity.Name;

            return View();
        }

        //POST: InvestimentUser/Create
        [HttpPost]
        public async Task<ActionResult> Create(InvestimentUser model)
        {
            string result = "";
            model.UserGUID = User.Identity.Name;
            model.InputDate = DateTime.Now;

            try
            {
                var cb = new InvestimentUserBusiness();

                result = await cb.AddAsync(model);

                ViewBag.InvestimentId = new SelectList(Db.Investiment.OrderBy(x => x.Id), "Id", "Name");

                TempData["MsgInvestimentoSucesso"] = "O investimento foi realizado com sucesso";

                return View();

                // TODO: Add insert logic here


                //if (cb.Error(result))
                //{
                //    ModelState.AddModelError("", result);

                //    ViewBag.InvestimentId = new SelectList(Db.Investiment.OrderBy(x => x.Id), "Id", "Name");
                //    return View(model);
                //}
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestimentUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InvestimentUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: InvestimentUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InvestimentUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
