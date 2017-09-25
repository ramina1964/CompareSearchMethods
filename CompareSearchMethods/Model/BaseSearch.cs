using CompareSearchMethods.GUI.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CompareSearchMethods.GUI.Model
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

		/************************************** Protected Properties ***************************************/
		public static Random Random;
		protected ObservableCollection<int> Data;

		/**************************************** Abstract Methods *****************************************/
		public abstract ISearchItem FindItem(int value);

		/******************************* Constants & Static & Readonly Fields ******************************/

		public static int MinNoOfEntries = (int)1e3;
		public static int MaxNoOfEntries = (int)1e7;
		public static int MinNoOfSearches = (int)1e2;
		public static int MaxNoOfSearches = (int)1e6;

		public static readonly string NoOfEntriesError =
			$"No of entries must be an integer in the interval [{MinNoOfEntries}, {MaxNoOfEntries}].";

		public static readonly string NoOfSearchesError =
			$"No. of searches must be an integer in the interval [{MinNoOfSearches}, {MaxNoOfSearches}].";

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
	}
}
