using SearchMethods.Model;

namespace SearchMethods.Model
{
    public class SearchItem : ISearchItem
    {
        public int? TargetIndex { get; set; }

        public int TargetValue { get; set; }

        public int NoOfIterations { get; set; }
    }
}
