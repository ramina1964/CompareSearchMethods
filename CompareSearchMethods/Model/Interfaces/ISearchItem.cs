namespace CompareSearchMethods.Model.Interfaces
{
	public interface ISearchItem
	{
		int? TargetIndex { get; set; }
		int TargetValue { get; set; }
		int NoOfIters { get; set; }
	}
}
