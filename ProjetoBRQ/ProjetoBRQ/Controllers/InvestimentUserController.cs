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

        [Authorize(Roles = "ADMIN")]
        public ActionResult Details(string email)
        {
            IList<Investiment> l = new List<Investiment>();
            var ius = Db.InvestimentUser.Where(x => x.UserGUID == email).FirstOrDefault();
            ius.TotalValue = ius.Num * ius.ValueInvestiment;
            Investiment investiment;
            foreach (var item in Db.InvestimentUser.Where(x => x.UserGUID == ius.UserGUID && x.Id != ius.Id))
            {
                ius.TotalValue += item.Num * item.ValueInvestiment;
                investiment = Db.Investiment.Find(item.InvestimentId);
                investiment.Num = item.Num;
                investiment.Total = item.Num * item.ValueInvestiment;
                l.Add(investiment);
                investiment = null;
            }

            var i = Db.Investiment.Find(ius.InvestimentId);
            i.Num = ius.Num;
            i.Total = ius.Num * ius.ValueInvestiment;
            l.Add(i);
            ius.Investiment = l;
            return View(ius);
        }

        [Authorize]
        //GET: InvestimentUser/Create
        public ActionResult Create()
        {
            ViewBag.InvestimentId = new SelectList(Db.Investiment.Where(x => !x.Deleted && x.Stock > 0).OrderBy(x => x.Id), "Id", "NameWithValue");
            ViewBag.CurrentUser = User.Identity.Name;

            return View();
        }

        //POST: InvestimentUser/Create
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(InvestimentUser model)
        {
            string result = "";
            model.UserGUID = User.Identity.Name;
            ViewBag.InvestimentId = new SelectList(Db.Investiment.Where(x => !x.Deleted && x.Stock > 0).OrderBy(x => x.Id), "Id", "NameWithValue");
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var cb = new InvestimentUserBusiness();
                result = await cb.AddAsync(model);
                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);
                    return View(model);
                }
                else
                {
                    TempData["MsgInvestimentoSucesso"] = "O investimento foi realizado com sucesso";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "ADMIN")]
        // GET: InvestimentUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DuvidasInvest()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public ActionResult Historico()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<JsonResult> GetTotalInvestiment()
        {
            double x= 0.0;
            try
            {
                x = await new InvestimentUserBusiness().GetTotalInvestimentAsync(); 
            }
            catch (Exception) { }

            return Json(new
            {
                x = x,

            },JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "ADMIN")]
        public JsonResult TableHistorico(int? Page, InvestimentUser model)
        {
            const int registers = 10;
            Page = Page ?? 1;
            Page--;
            IList<string> investiment = new List<string>();
            Investiment aux;

            var query = Db.InvestimentUser.Select(x => new
            {
                Email = x.UserGUID
            }).Distinct();

            if (model.UserGUID != null)
                query = query.Where(m => m.Email == model.UserGUID);

            query = query.Distinct();

           var arr = query.OrderByDescending(x => x.Email).Skip(Page.Value * registers).Take(registers).ToArray();
                
            var count = query.Count();
            int countPages = (int)(count / registers);

            return Json(new
            {
                list = arr,
                count = count,
                countPages = countPages,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
