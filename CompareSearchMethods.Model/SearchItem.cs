using CompareSearchMethods.Model;

namespace CompareSearchMethods.Model
{
	public class SearchItem : ISearchItem
	{
		/******************************************* Properties ********************************************/
		public int? TargetIndex { get; set; }
		public int TargetValue { get; set; }
		public int NoOfIterations { get; set; }
	}
}
