using ProjetoBRQ.Business;
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

        // GET: InvestimentUser/Create
        //public ActionResult Create()
        //{
          //  return View();
        //}

        // POST: InvestimentUser/Create
        //[HttpPost]
        public async Task<ActionResult> Create(InvestimentUser model)
        {
            string result = "";

            try
            {

                model = new InvestimentUser()
                {
                    InvestimentId = 1,
                    CardNumber = "0000 0000 0000 0000",
                    Num = 2,
                    UserGUID = "caiow.willian@gmail.com",
                };

                // TODO: Add insert logic here
                var cb = new InvestimentUserBusiness();
                result = await cb.AddAsync(model);

                //if (cb.Error(result))
                //{
                    ModelState.AddModelError("", result);
                    return View(model);
                //}
                return RedirectToAction("Index");
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
