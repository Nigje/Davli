
namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// Sort expression.
    /// </summary>
    public class SortExpression
    {
        //************************************************************************
        //Variables:

        /// <summary>
        /// Order by desending.
        /// </summary>
        public bool OrderByDesending { get; set; }

        /// <summary>
        /// Property name.
        /// </summary>
        public string PropertyName { get; set; }

        //************************************************************************
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sortObject"></param>
        public SortExpression(string sortObject)
        {
            var sortObjects = sortObject.Split(new char[] { '=' }, 2);
            PropertyName = sortObjects[0];
            OrderByDesending = sortObjects[1] == "asc";
        }
        //************************************************************************
    }
}
