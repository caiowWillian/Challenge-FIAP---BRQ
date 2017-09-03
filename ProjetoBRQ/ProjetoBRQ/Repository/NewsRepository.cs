using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.DAO
{
    public class NewsRepository
    {
        public int Add(News news)
        {
            string id = "-1";
            try
            {
                using (var db = new DbBRQ())
                {
                    using(var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                    {
                        OracleConnection connection = (OracleConnection)db.Database.Connection;
                        connection.Open();

                        cmd.CommandText = "sp_insert_news";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("title", OracleDbType.Varchar2, news.Title, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("body", OracleDbType.Varchar2, news.Body, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("description", OracleDbType.Varchar2, news.Description, ParameterDirection.Input));
                        cmd.Parameters.Add("result_img_news", OracleDbType.Int32).Direction = ParameterDirection.Output;

                        var i = cmd.ExecuteNonQuery();
                        id = cmd.Parameters["result_img_news"].Value.ToString();
                        connection.Close();
                    }
                }
            }catch (Exception) { }

            return Convert.ToInt32(id);
        }
    }
}