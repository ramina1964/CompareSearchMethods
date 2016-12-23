namespace CompareSearchMethods.Model
{
    public class BinarySearch : BaseSearch
    {
        /****************************************** Constructors *******************************************/
        public BinarySearch(int noOfEntries, int noOfSearches) : base(noOfEntries, noOfSearches)
        { }


        /*************************************** Overridden Methods ****************************************/
        public override SearchItem FindItem(int value)
        { return FindItemWithBinarySearch(value); }


        /**************************************** Private Methods ******************************************/
        private SearchItem FindItemWithBinarySearch(int value)
        {
            const int noOfIters = 0;
            return FindItemWithBinarySearch(0, NoOfEntries - 1, value, noOfIters);
        }

        private SearchItem FindItemWithBinarySearch(int low, int high, int value, int noOfIters)
        {
            noOfIters++;

            if (low > high)
                return new SearchItem()
                {
                    TargetIndex = null,
                    TargetValue = value,
                    NoOfIters = noOfIters
                };

            var mid = (low + high) / 2;
            if (Data[mid] == value)
                return new SearchItem()
                {
                    TargetIndex = mid,
                    TargetValue = value,
                    NoOfIters = noOfIters
                };

            return (Data[mid] > value)
                ? FindItemWithBinarySearch(low, mid - 1, value, noOfIters)
                : FindItemWithBinarySearch(mid + 1, high, value, noOfIters);
        }


    }
}
