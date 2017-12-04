namespace LiveScore.Models.Request
{
    public enum FilterRelation
    {
        OR = 0,
        AND = 1
    }

    public class Filter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public FilterRelation Relation { get; set; }
    }
}
