namespace CompareSearchMethods.Model
{
	public interface ISearchItem
	{
		int? TargetIndex { get; set; }

		int TargetValue { get; set; }

		int NoOfIterations { get; set; }
	}
}
