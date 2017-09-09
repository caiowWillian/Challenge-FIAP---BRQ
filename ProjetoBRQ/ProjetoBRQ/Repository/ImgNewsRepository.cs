using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Data;

namespace ProjetoBRQ.Repository
{
    public class ImgNewsRepository
    {
        public int Update(ImgNews ImgNews)
        {
            string result;
            using (var db = new DbBRQ())
            {
                using(var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                {
                    var connection = (OracleConnection)db.Database.Connection;
                    connection.Open();
                    cmd.CommandText = "sp_update_img_news";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, ImgNews.Id, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("v_file_content", OracleDbType.Blob, ImgNews.FileContent, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("v_file_name", OracleDbType.Varchar2, ImgNews.FileName, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("v_mime_type", OracleDbType.Varchar2, ImgNews.MimeType, ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("v_file_lenght", OracleDbType.Int32, ImgNews.FileLenght, ParameterDirection.Input));
                    cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    result = cmd.Parameters["result"].Value.ToString();
                }
            }
                return 0;
        }

        public int Add(ImgNews ImgNews, int idNews)
        {
            string id = "-1";
                using (var db = new DbBRQ())
                {
                    using (var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                    {
                        OracleConnection connection = (OracleConnection)db.Database.Connection;
                        connection.Open();
                        cmd.CommandText = "sp_insert_img_news";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("v_file_content", OracleDbType.Blob, ImgNews.FileContent, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_file_name", OracleDbType.Varchar2, ImgNews.FileName, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_id_noticia", OracleDbType.Int32, idNews, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_mime_type", OracleDbType.Varchar2, ImgNews.MimeType, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("v_file_lenght", OracleDbType.Int32, ImgNews.FileLenght, ParameterDirection.Input));
                        cmd.Parameters.Add("result_img_news", OracleDbType.Int32).Direction = ParameterDirection.Output;

                        var i = cmd.ExecuteNonQuery();
                        id = cmd.Parameters["result_img_news"].Value.ToString();
                        connection.Close();
                    }
                }

            return Convert.ToInt32(id);
        }
    }
}