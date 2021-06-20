using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SearchMethods.Model
{
    // Todo: Implement CancelSimulation here.

    public abstract class BaseSearch
    {
        protected BaseSearch(ISearchItem searchItem, int noOfEntries)
        {
            SearchItem = searchItem;
            NoOfEntries = noOfEntries;
            StartValue = 0;
            EndValue = 5 * NoOfEntries - 1;

            IndexOutOfRangeError = $"Index must be an integer in the interval [{0}, {NoOfEntries - 1}].";
            Random = new Random();
        }

        public ISearchItem SearchItem { get; }

        public int NoOfEntries
        { get; }

        public int EndValue { get; }

        public int StartValue { get; }

        public int this[int index]
        {
            get => Data[index];
            set
            {
                if (0 > index || index > NoOfEntries - 1)
                { throw new IndexOutOfRangeException(IndexOutOfRangeError); }

                Data[index] = value;
            }
        }
        public static Random Random;

        protected ObservableCollection<int> Data;

        public abstract ISearchItem FindItem(int value);

        public static string IndexOutOfRangeError;

        public void InitializeData()
        {
            var data = new HashSet<int>();
            var size = NoOfEntries;

            while (data.Count < size)
            { data.Add(Random.Next(StartValue, EndValue)); }

            var result = data.ToList();
            result.Sort();
            Data = new ObservableCollection<int>(result);
        }
    }
}
