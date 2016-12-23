namespace CompareSearchMethods.Model
{
	public class LinearSearch : BaseSearch
	{
		/****************************************** Constructors *******************************************/
		public LinearSearch(int noOfEntries, int noOfSearches) : base(noOfEntries, noOfSearches)
		{ }


		/*************************************** Overridden Methods ****************************************/
		public override SearchItem FindItem(int value)
		{
			int? targetIndex = null;
			var noOfIters = 0;
			for (var i = 0; i < NoOfEntries; i++)
			{
				noOfIters++;
				if (Data[i] < value)
					continue;

				if (Data[i] > value)
					break;

				targetIndex = i;
				break;
			}

			return new SearchItem()
			{
				TargetIndex = targetIndex,
				TargetValue = value,
				NoOfIters = noOfIters
			};
		}

	}
}
