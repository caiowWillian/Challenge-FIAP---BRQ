using ProjetoBRQ.Context;
using ProjetoBRQ.Interface;
using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using ProjetoBRQ.Utils;
using System;
using System.Threading.Tasks;

namespace ProjetoBRQ.Business
{
    public class InvestmentBusiness : IDisposable, OperationBD
    {
        private DbBRQ Db;

        public InvestmentBusiness()
        {
            Db = new DbBRQ();
        }

        public async Task<string> AddAsync(Investiment investiment)
        {
            var x = await new InvestmentRepository().AddAsync(investiment);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o contato, tente novamente";

            return result.ToString();
        }

        public async Task<dynamic> DeleteAsync(int Id)
        {
            var x = await new InvestmentRepository().DeleteAsync(Id);

            if (x != 0)
                return "Erro ao deletar";

            return "0";
        }

        public async Task<string> UpdateAsync(Investiment investiment)
        {
            var x = await new InvestmentRepository().UpdateAsync(investiment);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o contato, tente novamente";

            return result.ToString();
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public bool Error(string result)
        {
            if (!StringUtils.IsNumber(result))
                return true;
            return false;
        }
    }
}