using Davli.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Tools
{
    public static class StringExtentions
    {
        public static T ToEnum<T>(string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                throw new DavliExceptionInvalidParameter();
            }
        }
    }
}
