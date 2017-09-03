using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.DAO;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Business
{
    public class NewsBusiness
    {
        public int Add(News news)
        {
            return new NewsRepository().Add(news);
        }
    }
}