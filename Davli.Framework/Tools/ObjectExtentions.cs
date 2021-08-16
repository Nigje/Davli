using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Tools
{
    public static class ObjectExtentions
    {
        //********************************************************************************
        /// <summary>
        /// Convert an object to http-get query string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToQueryString(this object obj)
        {
            var result = new List<string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
            {
                result.Add(property.Name + "=" + property.GetValue(obj));
            }

            return string.Join("&", result);
        }
        //******************************************************************************** 
    }
}
