using ProjetoBRQ.DAO;
using ProjetoBRQ.Models;
using System.Threading.Tasks;

namespace ProjetoBRQ.Business
{
    public class NewsBusiness
    {
        public async Task<int> DeleteAsync(int Id)
        {
            
            return await new NewsRepository().DeleteAsync(Id);
        }

        public async Task<int> UpdateAsync(News News)
        {
            return await new NewsRepository().UpdateAsync(News);
        }

        public async Task<int> AddAsync(News News)
        {
            return await new NewsRepository().AddAsync(News);
        }
    }
}