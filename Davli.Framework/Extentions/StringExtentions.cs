using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Extentions
{
    public static class StringExtentions
    {
        //********************************************************************************
        /// <summary>
        /// Cast string to a type.
        /// </summary>
        public static T ToEnum<T>(this string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //********************************************************************************
    }
}
