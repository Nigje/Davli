using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// Filter-details to impose on the data list.
    /// </summary>
    [DataContract]
    public class Filter
    {
        //*************************************************************************
        //Variables:

        /// <summary>
        /// Less than condition.
        /// </summary>
        [DataMember(Name = "lt")]
        public object LessThan { get; set; }

        /// <summary>
        /// Less than or equal condition.
        /// </summary>
        [DataMember(Name = "lte")]
        public object LessThanOrEqual { get; set; }

        /// <summary>
        /// Greater than condition.
        /// </summary>
        [DataMember(Name = "gt")]
        public object GreaterThan { get; set; }

        /// <summary>
        /// Greater than or equal condition.
        /// </summary>
        [DataMember(Name = "gte")]
        public object GreaterThanOrEqual { get; set; }

        /// <summary>
        /// Equal condition.
        /// </summary>
        [DataMember(Name = "eq")]
        public object Equal { get; set; }

        /// <summary>
        /// Contain condition.
        /// </summary>
        [DataMember(Name = "ct")]
        public List<object> Contain { get; set; }

        //*************************************************************************
        /// <summary>
        /// Defualt constructor.
        /// </summary>
        public Filter()
        {

        }

        //*************************************************************************
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="obj"></param>
        public Filter(Filter obj)
        {
            LessThan = obj.LessThan;
            LessThanOrEqual = obj.LessThanOrEqual;
            GreaterThan = obj.GreaterThan;
            GreaterThanOrEqual = obj.GreaterThanOrEqual;
            Equal = obj.Equal;
            Contain = obj.Contain;
        }
    }
}
