using CompareSearchMethods.GUI.Model.Interfaces;

namespace CompareSearchMethods.GUI.Model
{
	public class LinearSearch : BaseSearch
	{
		/****************************************** Constructors *******************************************/
		public LinearSearch(ISearchItem searchItem, int noOfEntries)
			: base(searchItem, noOfEntries)
		{ }

		/*************************************** Overridden Methods ****************************************/
		public override ISearchItem FindItem(int value)
		{
			int? targetIndex = null;
			var noOfIterations = 0;
			for (var i = 0; i < NoOfEntries; i++)
			{
				noOfIterations++;
				if (Data[i] < value)
					continue;

				if (Data[i] > value)
					break;

				targetIndex = i;
				break;
			}

			SearchItem.TargetIndex = targetIndex;
			SearchItem.TargetValue = value;
			SearchItem.NoOfIterations = noOfIterations;
			return SearchItem;
		}

	}
}
