
namespace CompareSearchMethods.Model
{
	public interface ISimulationResults
	{
		int NoOfEntries { get; set; }
		long NoOfSearches { get; set; }
		double AvgNoOfIterations { get; set; }
		double AvgElapsedTime { get; set; }
	}
}
