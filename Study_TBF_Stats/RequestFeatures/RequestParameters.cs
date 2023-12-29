namespace Study_TBF_Stats.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPage = 50;
        public int pageNumber { get; set; } = 1;

        public int _pageSize = 5;
        public int pageSize { get { return _pageSize; } set { _pageSize = value > maxPage ? maxPage : value; } }
        public string? orderBy { get; set; }

    }
}
