using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadingSystem.Library
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string originalString)
        {
            if (string.IsNullOrWhiteSpace(originalString)) return true;
            return false;
        }

        public static bool IsNotEmpty(this string originalString)
        {
            if (string.IsNullOrWhiteSpace(originalString)) return false;
            return true;
        }
    }
}
