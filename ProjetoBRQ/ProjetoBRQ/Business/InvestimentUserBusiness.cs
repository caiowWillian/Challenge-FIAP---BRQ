using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using ProjetoBRQ.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjetoBRQ.Business
{
    public class InvestimentUserBusiness : IDisposable
    {

        private DbBRQ Db;

        public InvestimentUserBusiness()
        {
            Db = new DbBRQ();
        }

        public async Task<string> AddAsync(InvestimentUser investiment)
        {
            var x = await new InvestimentUserRepository().AddAsync(investiment);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Nenhum investimento encontrato, por favor tente novamente";
            if(Convert.ToInt32(result) == -2)
                result = "Houve um erro ao inserir o contato, tente novamente";
            if (Convert.ToInt32(result) == -3)
            {
                int? stock=0;
                stock = Db.Investiment.Find(investiment.InvestimentId).Stock;
                result = "Infelizmente temos apenas "+stock + " unidades em estoque";
            }
                
            return result.ToString();
        }

        public async Task<double> GetTotalInvestimentAsync()
        {
            return await new InvestimentUserRepository().GetTotalInvestimentAsync();
        }

        public bool Error(string result)
        {
            if (!StringUtils.IsNumber(result))
                return true;
            return false;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}