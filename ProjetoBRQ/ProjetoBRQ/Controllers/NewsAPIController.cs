using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjetoBRQ.Controllers
{
    public class NewsAPIController : ApiController
    {
        private DbBRQ db = new DbBRQ();

        public IEnumerable<News> GetNews(string title = null, int? id = null)
        {
            const int register = 10;

            db.Configuration.LazyLoadingEnabled = false;
            var query = db.News.Where(m => m.Deletado != 1);

            if (title != null)
                query = query.Where(m => m.Title == title);

            if (id != null)
                query = query.Where(m => m.Id == id);

            return query.OrderByDescending(m => m.Id).AsEnumerable();
        }
    }
}
