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
    public class ContatosBusiness : IDisposable
    {
        private DbBRQ Db;

        public ContatosBusiness()
        {
            Db = new DbBRQ();
        }

        public async Task<string> DeleteAsync(int Id)
        {
            var x = await new ContatosRepository().DeleteAsync(Id);

            if (x != 0)
                return "Erro ao deletar";

            return "0";
        }

        public async Task<string> AddAsync(Contatos contatos)
        {
            var x = await new ContatosRepository().AddAsync(contatos);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o contato, tente novamente";

            return result.ToString();
        }

        public async Task<string> UpdateAsync(Contatos contatos)
        {
            var x = await new ContatosRepository().UpdateAsync(contatos);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o contato, tente novamente";

            return result.ToString();
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