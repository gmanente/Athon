using System;
using System.Collections.Generic;

namespace Sistema.Web.API.SwaggerExt
{
    public class DescAlphaComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return y.CompareTo(x);
        }
    }
}