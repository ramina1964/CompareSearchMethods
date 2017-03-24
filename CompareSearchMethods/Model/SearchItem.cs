using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.Model
{
	public class SearchItem : ISearchItem
	{
		/******************************************* Properties ********************************************/
		public int? TargetIndex { get; set; }
		public int TargetValue { get; set; }
		public int NoOfIters { get; set; }
	}
}
