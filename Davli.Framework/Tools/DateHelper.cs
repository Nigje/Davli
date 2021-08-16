using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Tools
{
    public static class DateHelper
    {
        //Todo: get ?Datetime
        public static string ToPersianString(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)} {pc.GetHour(date)}:{pc.GetMinute(date)}:{pc.GetSecond(date)}" ;
        }
    }
}
