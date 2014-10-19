namespace net.the_engineers.elmar.everywhere.requests
{
    public class MergeEntityRequest
    {
        public int IntersectNodeId { get; set; }
        public string EntityGuid { get; set; }
        public int OtherIntersectNodeId { get; set; }
        public string OtherEntityGuid { get; set; }
    }
}