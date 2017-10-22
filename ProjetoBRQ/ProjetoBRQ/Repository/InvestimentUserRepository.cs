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
    public class InvestimentUserRepository : IDisposable
    {
        private DbBRQ Db;

        public InvestimentUserRepository()
        {
            Db = new DbBRQ();
        }

        public async Task<int> AddAsync(InvestimentUser investimentUser)
        {
            string id = "-1";
            using (var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_insert_investiment_user";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new OracleParameter("v_user_guid", OracleDbType.NVarchar2, investimentUser.UserGUID, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_num", OracleDbType.Int32, investimentUser.Num, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_card_number", OracleDbType.Varchar2, investimentUser.CardNumber, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("v_id_investiment", OracleDbType.Int32, investimentUser.InvestimentId, ParameterDirection.Input));

                cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                id = cmd.Parameters["result"].Value.ToString();
                connection.Close();
            }
            return Convert.ToInt32(id);
        }

        public async Task<double> GetTotalInvestimentAsync()
        {
            double total = 0;

            using(var cmd = Db.Database.Connection.CreateCommand() as OracleCommand)
            {
                OracleConnection connection = (OracleConnection)Db.Database.Connection;
                connection.Open();
                cmd.CommandText = "sp_get_total_investiment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("result", OracleDbType.Double).Direction = ParameterDirection.Output;
                await cmd.ExecuteNonQueryAsync();
                total = Convert.ToDouble(cmd.Parameters["result"].Value.ToString());
                connection.Close();
            }
            return total;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}