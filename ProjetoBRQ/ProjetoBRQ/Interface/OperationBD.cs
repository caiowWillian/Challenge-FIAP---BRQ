using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBRQ.Interface
{
    interface OperationBD
    {
        Task<dynamic> DeleteAsync(int Id);
        bool Error(string result);
    }
}
