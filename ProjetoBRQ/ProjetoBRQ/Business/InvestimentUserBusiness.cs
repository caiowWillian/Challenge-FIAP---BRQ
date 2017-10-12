using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjetoBRQ.Business
{
    public class InvestimentUserBusiness
    {

        public async Task<string> AddAsync(InvestimentUser investiment)
        {
            var x = await new InvestimentUserRepository().AddAsync(investiment);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o contato, tente novamente";

            return result.ToString();
        }
    }
}