
namespace Davli.Framework.Models.FilterAndPaging
{
    /// <summary>
    /// Metadata that describes a specific field member of a class
    /// </summary>
    public class MetadataField
    {
        /// <summary>
        /// The name of the field which this instance of <see cref="MetadataField"/> describes
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The title of the field
        /// TODO: Must be localizable
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The data-type of the field
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If true, the corresponding field is sortable. 
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// If true, the corresponding field is filterable
        /// </summary>
        public bool IsFilterable { get; set; }

    }
}
