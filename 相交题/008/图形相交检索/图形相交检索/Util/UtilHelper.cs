using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geone.Test.Util
{
    public static class UtilHelper
    {
        public static string Fmt(this  string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static bool IsNullOrEmpty(this  string str)
        {
            return string.IsNullOrEmpty(str);
        }

    }
}
