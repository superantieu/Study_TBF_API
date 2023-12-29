namespace Study_TBF_Stats.RequestFeatures
{
    public class ProjectParameters : RequestParameters
    {
        public int MinFloorArea { get; set; }
        public int MaxFloorArea { get; set; } = int.MaxValue;

        public bool? Completed { get; set; }
        public string? SearchTerm { get; set; }
    }
}
