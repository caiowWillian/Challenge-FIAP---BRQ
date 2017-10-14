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
    public class ImgNewsAPIController : ApiController
    {
        private DbBRQ Db = new DbBRQ();

        public IEnumerable<ImgNews> GetImgNews(int id)
        {
            Db.Configuration.LazyLoadingEnabled = false;

            return Db.ImgNews.Where(m => m.News.Id == id).AsEnumerable();

            //return db.ImgNews.Where(m => id.Contains(m.News.Id));
            //return db.ImgNews.Where(m => id.Contains(m)).AsEnumerable();


        }
    }
}
