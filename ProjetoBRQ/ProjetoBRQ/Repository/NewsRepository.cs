using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ProjetoBRQ.DAO
{
    public class NewsRepository
    {
        public async Task<int> DeleteAsync(int Id)
        {
            string result = "0";
            using(var db = new DbBRQ())
            {
                using(var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                {
                    var connection = (OracleConnection)db.Database.Connection;
                    connection.Open();
                    cmd.CommandText = "sp_delete_news";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, Id, ParameterDirection.Input));
                    cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    await cmd.ExecuteNonQueryAsync();
                    result = cmd.Parameters["result"].Value.ToString();
                    connection.Close();
                }
            }

            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateAsync(News News)
        {
            string id = "0";

                using(var db = new DbBRQ())
                {
                    using(var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                    {
                        var connection = (OracleConnection)db.Database.Connection;
                        connection.Open();
                        cmd.CommandText = "sp_update_news";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, News.Id, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_title", OracleDbType.Varchar2, News.Title, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_description", OracleDbType.Varchar2, News.Description, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_body", OracleDbType.Varchar2, News.Body, ParameterDirection.Input));
                        cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                        await cmd.ExecuteNonQueryAsync();
                        id = cmd.Parameters["result"].Value.ToString();
                        connection.Close();
                    }
                }

            return Convert.ToInt32(id);
        }

        public async Task<int> AddAsync(News news)
        {
            string id = "-1";
                using (var db = new DbBRQ())
                {
                    using(var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                    {
                        OracleConnection connection = (OracleConnection)db.Database.Connection;
                        connection.Open();

                        cmd.CommandText = "sp_insert_news";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("v_title", OracleDbType.Varchar2, news.Title, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_description", OracleDbType.Varchar2, news.Description, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_body", OracleDbType.Varchar2, news.Body, ParameterDirection.Input));
                        cmd.Parameters.Add("result_img_news", OracleDbType.Int32).Direction = ParameterDirection.Output;

                        await cmd.ExecuteNonQueryAsync();
                        id = cmd.Parameters["result_img_news"].Value.ToString();
                        connection.Close();
                    }
                }

            return Convert.ToInt32(id);
        }
    }
}