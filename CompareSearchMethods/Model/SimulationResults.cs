
namespace CompareSearchMethods.Model
{
	public class SimulationResults : ISimulationResults
	{
		public int NoOfEntries { get; set; }
		public long NoOfSearches { get; set; }
		public double AvgNoOfIterations { get; set; }
		public double AvgElapsedTime { get; set; }
	}
}
