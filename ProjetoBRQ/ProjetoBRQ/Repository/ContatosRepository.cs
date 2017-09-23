using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjetoBRQ.Repository
{
    public class ContatosRepository : IDisposable
    {
        private DbBRQ Db;

        public ContatosRepository()
        {
            Db = new DbBRQ();
        }

        public async Task<int> DeleteAsync(int id)
        {
            string result = "0";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                var connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_delete_contatos";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, id, ParameterDirection.Input));
                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                result = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateAsync(Contatos contatos)
        {
            string id = "-1";
            using(var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_update_contatos";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, contatos.Id, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_nome", OracleDbType.Varchar2, contatos.Nome, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_email", OracleDbType.Varchar2, contatos.Email, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_telefone", OracleDbType.Varchar2, contatos.Telefone, ParameterDirection.Input));
                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                id = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(id);
        }

        public async Task<int> AddAsync(Contatos contatos)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_insert_contato";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_nome", OracleDbType.Varchar2, contatos.Nome, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_email", OracleDbType.Varchar2, contatos.Email, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_telefone", OracleDbType.Varchar2, contatos.Telefone, ParameterDirection.Input));

                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                id = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(id);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}