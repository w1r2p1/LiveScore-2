namespace LiveScore.Models.Request
{
    public enum FilterRelation
    {
        OR = 0,
        AND = 1
    }

    public enum FilterType
    {
        StartDate = 0,
        EndDate = 1,
        Team = 2,
        Group = 3
    }

    public class Filter
    {
        public FilterType Name { get; set; }
        public string Value { get; set; }
        public FilterRelation Relation { get; set; }
    }
}
