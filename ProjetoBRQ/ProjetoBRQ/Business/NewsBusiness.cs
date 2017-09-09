using ProjetoBRQ.DAO;
using ProjetoBRQ.Models;

namespace ProjetoBRQ.Business
{
    public class NewsBusiness
    {
        public int Delete(int Id)
        {
            return new NewsRepository().Delete(Id);
        }

        public int Update(News News)
        {
            return new NewsRepository().Update(News);
        }

        public int Add(News News)
        {
            return new NewsRepository().Add(News);
        }
    }
}