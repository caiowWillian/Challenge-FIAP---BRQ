using Oracle.ManagedDataAccess.Client;
using ProjetoBRQ.Context;
using ProjetoBRQ.Controllers;
using ProjetoBRQ.Interface;
using ProjetoBRQ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjetoBRQ.Repository
{
    public class InvestmentRepository : IDisposable, OperationBD
    {
        private DbBRQ Db;

        public InvestmentRepository()
        {
            Db = new DbBRQ();
        }

        public async Task<dynamic> DeleteAsync(int Id)
        {
            string result = "0";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                var connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_delete_investiment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, Id, ParameterDirection.Input));
                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                result = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(result);
        }

        public async Task<int> UpdateAsync(Investiment Investment)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_update_investiment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_id", OracleDbType.Int32, Investment.Id, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_name", OracleDbType.Varchar2, Investment.Name, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_description", OracleDbType.Varchar2, Investment.Description, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_stock", OracleDbType.Varchar2, Investment.Stock, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_value", OracleDbType.Varchar2, Investment.Value, ParameterDirection.Input));
                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                id = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(id);
        }

        public async Task<int> AddAsync(Investiment Investment)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_insert_investment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_name", OracleDbType.Varchar2, Investment.Name, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_description", OracleDbType.Varchar2, Investment.Description, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_stock", OracleDbType.Varchar2, Investment.Stock, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_value", OracleDbType.Varchar2, Investment.Value, ParameterDirection.Input));

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

        public bool Error(string result)
        {
            throw new NotImplementedException();
        }
    }
}