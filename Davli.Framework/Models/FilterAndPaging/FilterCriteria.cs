using Newtonsoft.Json;

namespace Davli.Framework.Models.FilterAndPaging
{
    public class FilterCriteria
    {
        //*************************************************************************
        //Variables:

        /// <summary>
        /// Property name.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Filter object that contains condition details.
        /// </summary>
        public Filter Filter { get; set; }

        //*************************************************************************
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filterObject"></param>
        public FilterCriteria(string filterObject)
        {
            var filterObjects = filterObject.Split(new char[] { '=' }, 2);
            PropertyName = filterObjects[0];
            Filter = JsonConvert.DeserializeObject<Filter>(filterObjects[1]);
        }
        //*************************************************************************
    }
}
