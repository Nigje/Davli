using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Tools
{
    public static class OtpGenerator
    {
        /// <summary>
        /// Generate Otp.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateOtp(int length = 4)
        {
            DateTime nowDate = DateTime.Now;
            string ticks = nowDate.Ticks.ToString();
            string verifyCode = ticks.Substring(ticks.Length - length, length);
            return verifyCode;
        }
    }
}
