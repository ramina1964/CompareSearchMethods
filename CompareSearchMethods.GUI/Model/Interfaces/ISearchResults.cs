namespace CompareSearchMethods.Model.Interfaces
{
	public interface ISearchResults
	{
		int TotalNoOfIters { get; set; }
		double TotalElapsedTime { get; set; }
		double AvgNoOfIters { get; set; }
		double AvgElapsedTime { get; set; }
	}
}
