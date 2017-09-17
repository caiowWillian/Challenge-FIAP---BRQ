using ProjetoBRQ.Context;
using ProjetoBRQ.Models;
using ProjetoBRQ.Repository;
using ProjetoBRQ.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoBRQ.Business
{
    public class ClienteBusiness : IDisposable
    {
        private DbBRQ Db;

        public ClienteBusiness()
        {
            Db = new DbBRQ();
        }

        public async Task<string> DeleteAsync(int Id)
        {
            var x = await new ClientesRepository().DeleteAsync(Id);

            if (x != 0)
                return "Erro ao deletar";

            return "0";
        }

        public async Task<string> UpdateAsync(Cliente Cliente)
        {
            if (Db.Cliente.Where(m => m.Cpf == Cliente.Cpf && m.Id != Cliente.Id).FirstOrDefault() != null)
                return "CPF já cadastrado";

            if (Db.Cliente.Where(m => m.Email == Cliente.Email && m.Id != Cliente.Id).FirstOrDefault() != null)
                return "Email já cadastrado";

            var x = await new ClientesRepository().UpdateAsync(Cliente);
            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o cliente, tente novamente";

            return result.ToString();
        }

        public async Task<string> AddAsync(Cliente Cliente)
        {
            if (Db.Cliente.Where(m => m.Cpf == Cliente.Cpf).FirstOrDefault() != null)
                return "CPF já cadastrado";

            if (Db.Cliente.Where(m => m.Email == Cliente.Email).FirstOrDefault() != null)
                return "Email já cadastrado";

            var x = await new ClientesRepository().AddAsync(Cliente);

            var result = x.ToString();

            if (Convert.ToInt32(result) == -1)
                result = "Houve um erro ao inserir o cliente, tente novamente";

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