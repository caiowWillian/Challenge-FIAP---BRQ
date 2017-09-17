using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoBRQ.Utils
{
    public class StringUtils
    {
        public static bool IsNumber(string s)
        {
            try
            {
                Convert.ToInt32(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}