using CompareSearchMethods.Model;

namespace CompareSearchMethods.Model
{
	public class BinarySearch : BaseSearch
	{
		/****************************************** Constructors *******************************************/
		public BinarySearch(ISearchItem searchItem, int noOfEntries)
			: base(searchItem, noOfEntries)
		{ }

		/*************************************** Overridden Methods ****************************************/
		public override ISearchItem FindItem(int value)
		{ return FindItemWithBinarySearch(value); }

		/**************************************** Private Methods ******************************************/
		private ISearchItem FindItemWithBinarySearch(int value)
		{
			const int noOfIterations = 0;
			return FindItemWithBinarySearch(0, NoOfEntries - 1, value, noOfIterations);
		}

		private ISearchItem FindItemWithBinarySearch(int low, int high, int value, int noOfIterations)
		{
			noOfIterations++;

			if (low > high)
				return new SearchItem()
				{
					TargetIndex = null,
					TargetValue = value,
					NoOfIterations = noOfIterations
				};

			var mid = (low + high) / 2;
			if (Data[mid] == value)
				return new SearchItem()
				{
					TargetIndex = mid,
					TargetValue = value,
					NoOfIterations = noOfIterations
				};

			return (Data[mid] > value)
				? FindItemWithBinarySearch(low, mid - 1, value, noOfIterations)
				: FindItemWithBinarySearch(mid + 1, high, value, noOfIterations);
		}
	}
}
