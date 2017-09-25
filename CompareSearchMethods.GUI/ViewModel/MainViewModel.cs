using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompareSearchMethods.GUI.Commands;
using CompareSearchMethods.Model;
using CompareSearchMethods.Model.Interfaces;

namespace CompareSearchMethods.GUI.ViewModel
{
	/********************************************* Constructors ********************************************/
	public class MainViewModel : ViewModelBase
	{
		public MainViewModel(ISearchItem searchItem)
		{
			SearchItem = searchItem;
			SimulateCommand = new RelayCommand(CanSimulate, Simulate);
			CancelCommand = new RelayCommand(CanCancel, Cancel);

			NoOfEntries = (int)5e5;
			NoOfSearches = (int)1e3;

			IsSimulating = false;
			ProgressBarVisibility = Visibility.Collapsed;
		}

		/************************************** Static and Constants ***************************************/
		public static long MinProductValue = (long)1e5;
		public static long MaxProductValue = (long)5e10;
		public static readonly string MaxProductError =
			$"Product of No. of searches and No. of entries must be in the interval [{MinProductValue}, {MaxProductValue}].";

		/************************************ Public Events & Delegates ************************************/
		public ICommand SimulateCommand { get; set; }
		public ICommand CancelCommand { get; set; }

		/******************************************* Properties ********************************************/
		public ISearchItem SearchItem { get; set; }
		public bool IsSimulating { get; set; }
		public bool IsReady => !IsSimulating;

		public Visibility ProgressBarVisibility { get; set; }

		public double ProgressBarValue { get; set; }

		public string ProgressBarText => Math.Round(ProgressBarValue, 0) + "%";

		public ISimulationResults LinearSearchResults { get; set; }
		public ISimulationResults BinarySearchResults { get; set; }

		public int NoOfEntries
		{
			get => _noOfEntries;

			set
			{
				_noOfEntries = value;
				OnPropertyErrorsChanged(nameof(NoOfSearches));
			}
		}

		public int NoOfSearches
		{
			get => _noOfSearches;
			set
			{
				_noOfSearches = value;
				OnPropertyErrorsChanged(nameof(NoOfSearches));
			}
		}

		public int TargetValue
		{
			get => _targetValue;

			set
			{
				_targetValue = value;
				TargetIndex = BinarySearchType.FindItem(value).TargetIndex;
			}
		}

		public int? TargetIndex { get; set; }

		public string Entries { get; set; }

		private LinearSearch LinearSearchType { get; set; }

		private BinarySearch BinarySearchType { get; set; }


		/***************************************** Private Methods *****************************************/
		private void Validate()
		{
			Task.Run(() => DataValidation());
		}

		private void DataValidation()
		{
			#region NoOfEntries
			//Validate No. of Entries property
			if (PropErrors.TryGetValue(nameof(NoOfEntries), out var listErrors) == false)
				listErrors = new List<string>();

			else
				listErrors.Clear();

			if (string.IsNullOrEmpty(nameof(NoOfEntries)))
				listErrors.Add("No. of Entries is required.");

			if (BaseSearch.MinNoOfEntries > NoOfEntries || NoOfEntries > BaseSearch.MaxNoOfEntries)
				listErrors.Add(BaseSearch.NoOfEntriesError);

			PropErrors[nameof(NoOfEntries)] = listErrors;

			if (listErrors.Count > 0)
			{
				OnPropertyErrorsChanged(nameof(NoOfEntries));
			}
			#endregion NoOfEntries

			#region NoOfSearches
			//Validate No. of Searches property
			if (PropErrors.TryGetValue(nameof(NoOfSearches), out listErrors) == false)
				listErrors = new List<string>();

			else
				listErrors.Clear();

			if (string.IsNullOrEmpty(nameof(NoOfSearches)))
				listErrors.Add("No. of Searches is required.");

			if (BaseSearch.MinNoOfSearches > NoOfSearches || NoOfSearches > BaseSearch.MaxNoOfSearches)
				listErrors.Add(BaseSearch.NoOfSearchesError);

			PropErrors[nameof(NoOfSearches)] = listErrors;

			if (listErrors.Count > 0)
			{
				OnPropertyErrorsChanged(nameof(NoOfSearches));
			}
			#endregion NoOfSearches
		}

		private void Simulate(object obj)
		{
			IsSimulating = true;
			ProgressBarVisibility = Visibility.Visible;
			LinearSearchType = new LinearSearch(SearchItem, NoOfEntries);
			BinarySearchType = new BinarySearch(SearchItem, NoOfEntries);
			var searches = new BaseSearch[] { LinearSearchType, BinarySearchType };

			new Thread(() => Simulate(searches)).Start();
			IsSimulating = false;
		}

		private void Cancel(object obj)
		{

		}

		private bool CanSimulate(object obj)
		{ return IsReady && IsInputValid(); }

		private bool CanCancel(object obj)
		{ return IsSimulating; }

		private async void Simulate(params BaseSearch[] searchTypes)
		{
			IsSimulating = true;
			searchTypes[0].InitializeData();
			searchTypes[1].InitializeData();

			Entries = GetEntries();
			LinearSearchResults = await SimulateLinearSearchAsync(searchTypes[0]).ConfigureAwait(true);
			BinarySearchResults = await SimulateBinarySearchAsync(searchTypes[1]).ConfigureAwait(true);
			IsSimulating = false;
		}

		private Task<ISimulationResults> SimulateLinearSearchAsync(BaseSearch linearSearch)
		{
			return Task.Factory.StartNew(() =>
			{
				var totalNoOfIterations = 0.0;
				var start = DateTime.Now;
				for (var j = 0; j < NoOfSearches; j++)
				{
					var value = BaseSearch.Random.Next(linearSearch.StartValue, linearSearch.EndValue);
					var searchItem = linearSearch.FindItem(value);
					totalNoOfIterations += searchItem.NoOfIterations;
					ProgressBarValue = (j + 1) * 100.0 / NoOfSearches;
				}
				var end = DateTime.Now;
				ProgressBarValue = 0;
				ProgressBarVisibility = Visibility.Collapsed;

				var totalElapsedTime = (end - start).TotalMilliseconds;
				return SimulationResults(totalNoOfIterations, totalElapsedTime);
			});
		}

		private Task<ISimulationResults> SimulateBinarySearchAsync(BaseSearch binarySearch)
		{
			return Task.Factory.StartNew(() =>
			{
				TargetValue = binarySearch[0];
				var entries = binarySearch.FindItem(TargetValue);
				TargetIndex = entries.TargetIndex;

				var start = DateTime.Now;
				var totalNoOfIterations = 0.0;
				for (var j = 0; j < NoOfSearches; j++)
				{
					var value = BaseSearch.Random.Next(binarySearch.StartValue, binarySearch.EndValue);
					var searchItem = binarySearch.FindItem(value);
					totalNoOfIterations += searchItem.NoOfIterations;
				}
				var end = DateTime.Now;

				var totalElapsedTime = (end - start).TotalMilliseconds;
				return SimulationResults(totalNoOfIterations, totalElapsedTime);
			});
		}

		private ISimulationResults SimulationResults(double totalNoOfIterations, double totalElapsedTime)
		{
			return new SimulationResults()
			{
				NoOfEntries = NoOfEntries,
				NoOfSearches = NoOfSearches,
				AvgNoOfIterations = totalNoOfIterations / NoOfSearches,
				AvgElapsedTime = totalElapsedTime / NoOfSearches
			};
		}

		private string GetEntries()
		{
			if (NoOfEntries <= 32)
			{
				return GetSubString(0, NoOfEntries - 1).ToString();
			}

			const string subEtc = " ..., ";
			var mid = NoOfEntries / 2;

			var startStr = GetSubString(0, 4);
			var midStr = GetSubString(mid, mid + 2);
			var endStr = GetSubString(NoOfEntries - 2, NoOfEntries - 1);

			return startStr.Append(subEtc).Append(midStr).Append(subEtc).Append(endStr).ToString();
		}

		private StringBuilder GetSubString(int fromIndex, int toIndex)
		{
			var sb = new StringBuilder();
			for (var i = fromIndex; i < toIndex; i++)
			{
				sb.Append(BinarySearchType[i] + ", ");
			}

			return (toIndex == NoOfEntries - 1)
				? sb.Append(BinarySearchType[toIndex])
				: sb.Append(BinarySearchType[toIndex] + ", ");
		}

		private bool IsInputValid()
		{
			Validate();
			return !HasErrors;

			//var productValue = NoOfEntries * NoOfSearches;
			//var isNoOfEntriesInRange = BaseSearch.MinNoOfEntries <= NoOfEntries && NoOfEntries <= BaseSearch.MaxNoOfEntries;
			//var isNoOfSearchesInRange = BaseSearch.MinNoOfSearches <= NoOfSearches && NoOfSearches <= BaseSearch.MaxNoOfSearches;
			//var isProductInRange = MinProductValue <= productValue && productValue <= MaxProductValue;

			//return isNoOfEntriesInRange && isNoOfSearchesInRange && isProductInRange;
		}

		/***************************************** Private Fields ******************************************/
		private int _targetValue;

		private int _noOfEntries;
		private int _noOfSearches;
	}
}
