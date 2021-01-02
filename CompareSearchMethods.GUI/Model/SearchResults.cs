using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.Model
{
	public class SearchResults : ISearchResults
	{
		public int TotalNoOfIters { get; set; }
		public double TotalElapsedTime { get; set; }
		public double AvgNoOfIters { get; set; }
		public double AvgElapsedTime { get; set; }
	}
}
