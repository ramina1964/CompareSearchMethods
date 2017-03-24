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
		protected BaseSearch(ISearchItem searchItem, int noOfEntries, int noOfSearches)
		{
			SearchItem = searchItem;
			NoOfEntries = noOfEntries;
			NoOfSearches = noOfSearches;
			StartValue = 0;
			EndValue = 5 * NoOfEntries - 1;

			IndexOutOfRangeError = $"Index must be an integer in the interval [{0}, {NoOfEntries - 1}].";
			Rand = new Random();
		}

		/******************************************* Properties ********************************************/
		public ISearchItem SearchItem { get; }

		public int NoOfEntries
		{
			get { return _noOfEntries; }
			set
			{
				if (MinNoOfEntries > value || value > MaxNoOfEntries)
					throw new ArgumentOutOfRangeException(nameof(value), NoOfEntries, NoOfEntriesError);

				_noOfEntries = value;
			}
		}

		public int NoOfSearches
		{
			get { return _noOfSearches; }
			set
			{
				if (MinNoOfSearches > value || value > MaxNoOfSearches)
					throw new ArgumentOutOfRangeException(nameof(value), NoOfSearches, NoOfSearchesError);

				_noOfSearches = value;
			}
		}

		public int EndValue { get; }

		public int StartValue { get; }

		public int ElementAt(int index)
		{
			if (0 > index || index >= NoOfEntries)
				throw new IndexOutOfRangeException(IndexOutOfRangeError);

			return Data[index];
		}


		/************************************** Protected Properties ***************************************/
		public static Random Rand;
		protected ObservableCollection<int> Data;


		/**************************************** Abstract Methods *****************************************/
		public abstract ISearchItem FindItem(int value);


		/******************************* Constants & Static & Readonly Fields ******************************/
		public const int MaxNoOfEntries = (int) 1e7;
		public const int MinNoOfEntries = (int) 1e3;
		public const int MinNoOfSearches = (int) 1e2;
		public const int MaxNoOfSearches = (int) 1e4;

		public static readonly string NoOfEntriesError =
			$"No of Entries must be an integer in the interval [{MinNoOfEntries}, {MaxNoOfEntries}]";

		public static readonly string NoOfSearchesError =
			$"No. of searches must be an integer in the interval [{MinNoOfSearches}, {MaxNoOfSearches}].";

		public static readonly string NumericFormatError = "The entered value is not a numeric constant.";

		public static string IndexOutOfRangeError;


		/***************************************** Public Methods ******************************************/
		public int? HighestIndexOf(int item)
		{
			var searchItem = FindItem(item);
			var value = searchItem.TargetValue;
			var index = searchItem.TargetIndex;

			if (index == null)
			{ return null; }

			var idx = index.Value;
			for (var i = index.Value + 1; i < NoOfEntries; i++)
			{
				if (Data[i] != value)
				{ break; }

				idx++;
			}

			return idx;
		}

		public static void InitializeData(params BaseSearch[] searches)
		{
			var data = new List<int>();
			var size = searches[0].NoOfEntries;
			var startValue = searches[0].StartValue;
			var endValue = searches[0].EndValue;

			for (var i = 0; i < size; i++)
			{ data.Add(Rand.Next(startValue, endValue)); }

			data.Sort();
			foreach (var search in searches)
			{ search.Data = new ObservableCollection<int>(data.ToList()); }
		}


		/****************************************** Private Fields *****************************************/
		private int _noOfEntries;
		private int _noOfSearches;

	}
}
