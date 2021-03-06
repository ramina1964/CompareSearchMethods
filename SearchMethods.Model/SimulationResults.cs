namespace SearchMethods.Model
{
	public class SimulationResults : ISimulationResults
	{
		public int NoOfEntries { get; set; }

		public int NoOfSearches { get; set; }

		public double AvgNoOfIterations { get; set; }

		public double AvgElapsedTime { get; set; }
	}
}
