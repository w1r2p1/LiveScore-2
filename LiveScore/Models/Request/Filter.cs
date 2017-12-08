namespace LiveScore.Models.Request
{
    /// <summary>
    /// Filter relation enumeration
    /// </summary>
    public enum FilterRelation
    {
        OR = 0,
        AND = 1
    }

    /// <summary>
    /// Filter field type enumeration
    /// </summary>
    public enum FilterType
    {
        StartDate = 0,
        EndDate = 1,
        Team = 2,
        Group = 3
    }

    /// <summary>
    /// Request model that encapsulates filter-related data.
    /// </summary>
    public class Filter
    {
        /// <summary>Filter field name</summary>
        public FilterType Name { get; set; }

        /// <summary>Filter field value</summary>
        public string Value { get; set; }

        /// <summary>Filter relation</summary>
        public FilterRelation Relation { get; set; }
    }
}
