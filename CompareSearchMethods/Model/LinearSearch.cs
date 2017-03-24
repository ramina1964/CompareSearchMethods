using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.Model
{
	public class LinearSearch : BaseSearch
	{
		/****************************************** Constructors *******************************************/
		public LinearSearch(ISearchItem searchItem, int noOfEntries, int noOfSearches)
			: base(searchItem, noOfEntries, noOfSearches)
		{ }

		/*************************************** Overridden Methods ****************************************/
		public override ISearchItem FindItem(int value)
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

			SearchItem.TargetIndex = targetIndex;
			SearchItem.TargetValue = value;
			SearchItem.NoOfIters = noOfIters;
			return SearchItem;
		}

	}
}
