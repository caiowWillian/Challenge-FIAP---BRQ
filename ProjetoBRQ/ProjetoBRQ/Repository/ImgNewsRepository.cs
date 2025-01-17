﻿using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ProjetoBRQ.Repository
{
    public class ImgNewsRepository
    {
        public async Task<int> UpdateAsync(ImgNews ImgNews)
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

                    await cmd.ExecuteNonQueryAsync();

                    result = cmd.Parameters["result"].Value.ToString();
                }
            }
                return 0;
        }

        public async Task<int> AddAsync(ImgNews ImgNews, int idNews)
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

                        await cmd.ExecuteNonQueryAsync();
                        id = cmd.Parameters["result_img_news"].Value.ToString();
                        connection.Close();
                    }
                }

            return Convert.ToInt32(id);
        }
    }
}