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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Novidades()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CriarContato(Contatos model)
        {
            string result = "";
            if (!ModelState.IsValid)
            {
                TempData["scroolPosition"] = true;

                return View("Index", model);
            }

            try
            {
                var cb = new ContatosBusiness();
                result = await cb.AddAsync(model);

                if (cb.Error(result))
                {
                    ModelState.AddModelError("", result);

                    TempData["scroolPosition"] = true;

                    return View("Index", model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                TempData["scroolPosition"] = true;

                return View("Index", model);
            }

            TempData["scroolPosition"] = true;
            TempData["msgContatoSucesso"] = "Seu contato foi registrado com sucesso, iremos mante-lo informado de novidades";

            return View("Index");
        }
    }
}