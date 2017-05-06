using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.Model
{
	public abstract class BaseSearch
	{
		/****************************************** Constructors *******************************************/
		protected BaseSearch(ISearchItem searchItem, int noOfEntries)
		{
			SearchItem = searchItem;
			NoOfEntries = noOfEntries;
			StartValue = 0;
			EndValue = 5 * NoOfEntries - 1;

			IndexOutOfRangeError = $"Index must be an integer in the interval [{0}, {NoOfEntries - 1}].";
			Random = new Random();
		}

		/******************************************* Properties ********************************************/
		public ISearchItem SearchItem { get; }

		public int NoOfEntries
		{
			get => _noOfEntries;
			set
			{
				if (!Enumerable.Range(MinNoOfEntries, MaxNoOfEntries).Contains(value))

					throw new ArgumentOutOfRangeException(nameof(value), NoOfEntries, NoOfEntriesError);

				_noOfEntries = value;
			}
		}

		public int EndValue { get; }
		public int StartValue { get; }

		public int ElementAt(int index)
		{
			if (!Enumerable.Range(0, NoOfEntries).Contains(index))
			{ throw new IndexOutOfRangeException(IndexOutOfRangeError); }

			return Data[index];
		}

		/************************************** Protected Properties ***************************************/
		public static Random Random;
		protected ObservableCollection<int> Data;

		/**************************************** Abstract Methods *****************************************/
		public abstract ISearchItem FindItem(int value);

		/******************************* Constants & Static & Readonly Fields ******************************/
		public const int MaxNoOfEntries = (int)1e7;
		public const int MinNoOfEntries = (int)1e3;
		public const int MinNoOfSearches = (int)1e2;
		public const int MaxNoOfSearches = (int)1e4;

		public static readonly string NoOfEntriesError =
			$"No of Entries must be an integer in the interval [{MinNoOfEntries}, {MaxNoOfEntries}]";

		public static readonly string NoOfSearchesError =
			$"No. of searchType must be an integer in the interval [{MinNoOfSearches}, {MaxNoOfSearches}].";

		public static readonly string NumericFormatError = "The entered value is not a numeric constant.";

		public static string IndexOutOfRangeError;

		/***************************************** Public Methods ******************************************/
		public void InitializeData()
		{
			var data = new HashSet<int>();
			var size = NoOfEntries;
			var startValue = StartValue;
			var endValue = EndValue;

			while (data.Count < size)
			{ data.Add(Random.Next(startValue, endValue)); }

			var result = data.ToList();
			result.Sort();
			Data = new ObservableCollection<int>(result);
		}

		/****************************************** Private Fields *****************************************/
		private int _noOfEntries;
	}
}
