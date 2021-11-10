using System;
using System.Text.RegularExpressions;

namespace Commons
{
    public static class Validate
    {
        public static Boolean isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[A-Z]{3}[0-9]{3}$");
            return rg.IsMatch(strToCheck);
        }

        public static string ConvertToUppercase(string str)
        {
            return str.ToUpper();
        }

        public static bool ByteNumericChecker(string str, out byte convertType)
        {
            bool returnResult = byte.TryParse(str, out convertType);
            return returnResult;
        }

        public static bool IntNumericChecker(string str, out int convertType)
        {
            bool returnResult = int.TryParse(str, out convertType);
            return returnResult;
        }
        public static bool Range(int num, int range1, int range2)
        {
            if (num < range1 || num > range2)
            {
                return false;
            }
            return true;
        }
        
        
    }
}
