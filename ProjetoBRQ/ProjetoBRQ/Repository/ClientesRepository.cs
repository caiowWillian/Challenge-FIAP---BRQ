using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ProjetoBRQ.Repository
{
    public class ClientesRepository : IDisposable
    {
        private DbBRQ Db;

        public ClientesRepository()
        {
            Db = new DbBRQ();
        }

        public async Task<int> DeleteAsync(int Id)
        {
            string result = "0";
            using (var db = new DbBRQ())
            {
                using (var cmd = db.Database.Connection.CreateCommand() as OracleCommand)
                {
                    var connection = (OracleConnection)db.Database.Connection;
                    connection.Open();
                    cmd.CommandText = "sp_delete_clientes_brq";
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

        public async Task<int> UpdateAsync(Cliente Cliente)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_update_clientes_brq";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, Cliente.Id, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_nome", OracleDbType.Varchar2, Cliente.Nome, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_email", OracleDbType.Varchar2, Cliente.Email, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_telefone", OracleDbType.Varchar2, Cliente.Telefone, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_endereco", OracleDbType.Varchar2, Cliente.Endereco, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_complemento", OracleDbType.Varchar2, Cliente.Complemento, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_cpf", OracleDbType.Varchar2, Cliente.Cpf, ParameterDirection.Input));

                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                id = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(id);
        }

        public async Task<int> AddAsync(Cliente Cliente)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_insert_clientes_brq";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_nome", OracleDbType.Varchar2, Cliente.Nome, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_email", OracleDbType.Varchar2, Cliente.Email, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_telefone", OracleDbType.Varchar2, Cliente.Telefone, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_endereco", OracleDbType.Varchar2, Cliente.Endereco, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_complemento", OracleDbType.Varchar2, Cliente.Complemento, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_cpf", OracleDbType.Varchar2, Cliente.Cpf, ParameterDirection.Input));

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