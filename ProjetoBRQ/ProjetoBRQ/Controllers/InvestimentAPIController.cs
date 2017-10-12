using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ProjetoBRQ.Controllers
{
    public class InvestimentAPIController : ApiController
    {
        private DbBRQ db = new DbBRQ();

        [System.Web.Http.HttpGetAttribute]
        public IEnumerable<Investiment> GetInvestiment()
        {

            var teste = db.Investiment.ToList();

            return db.Investiment.AsEnumerable();
        }

    }
}
