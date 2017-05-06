using System;
using System.Text;
using System.Threading;
using System.Windows;
using CompareSearchMethods.Commands;
using CompareSearchMethods.Model;
using CompareSearchMethods.Model.Interfaces;
using PropertyChanged;

namespace CompareSearchMethods.ViewModel
{
	/********************************************* Constructors ********************************************/
	[ImplementPropertyChanged]
	public class MainViewModel
	{
		public MainViewModel(ISearchItem searchItem)
		{
			SearchItem = searchItem;
			CommandSimulate = new DelegateCommand(ExecuteSimulate, CanExecuteSimulate);
			CommandCancel = new DelegateCommand(ExecuteCancel, CanExecuteCancel);

			NoOfEntries = (int) 5e5;
			NoOfSearches = (int) 1e3;

			ProgressBarVisibility = Visibility.Collapsed;
		}

		public ISearchItem SearchItem { get; set; }


		/************************************ Public Events & Delegates ************************************/
		public DelegateCommand CommandSimulate { get; set; }

		public DelegateCommand CommandCancel { get; set; }


		/******************************************* Properties ********************************************/
		public bool CanSimulate =>
			(BaseSearch.MinNoOfEntries <= NoOfEntries && NoOfEntries <= BaseSearch.MaxNoOfEntries) &&
			(BaseSearch.MinNoOfSearches <= NoOfSearches && NoOfSearches <= BaseSearch.MaxNoOfSearches);

		public bool CanCancel => !CanSimulate;

		public Visibility ProgressBarVisibility { get; set; }

		public double ProgressBarValue { get; set; }

		public string ProgressBarText => Math.Round(ProgressBarValue, 0) + "%";

		public ISimulationResults LinearSearchResults { get; set; }
		public ISimulationResults BinarySearchResults { get; set; }

		public int NoOfEntries { get; set; }

		public int NoOfSearches { get; set; }

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
		private void ExecuteSimulate(object obj)
		{
			ProgressBarVisibility = Visibility.Visible;
			LinearSearchType = new LinearSearch(SearchItem, NoOfEntries);
			BinarySearchType = new BinarySearch(SearchItem, NoOfEntries);
			var searches = new BaseSearch[] {LinearSearchType, BinarySearchType};

			new Thread(() => Simulate(searches)).Start();
		}

		private void ExecuteCancel(object obj)
		{
			// Cancel the simulation.
		}

		private bool CanExecuteSimulate(object obj)

		{
			return CanSimulate;
		}

		private bool CanExecuteCancel(object obj)
		{
			return true;
		}

		private void Simulate(params BaseSearch[] searchTypes)
		{
			searchTypes[0].InitializeData();
			searchTypes[1].InitializeData();

			Entries = GetEntries();
			LinearSearchResults = SimulateLinearSearch(searchTypes[0]);
			BinarySearchResults = SimulateBinarySearch(searchTypes[1]);
		}

		private ISimulationResults SimulateLinearSearch(BaseSearch linearSearch)
		{
			const double tolerance = 1E-6;
			var totalNoOfIterations = 0.0;
			var start = DateTime.Now;
			for (var j = 0; j < NoOfSearches; j++)
			{
				var value = BaseSearch.Random.Next(linearSearch.StartValue, linearSearch.EndValue);
				var searchItem = linearSearch.FindItem(value);
				totalNoOfIterations += searchItem.NoOfIters;
				ProgressBarValue = (j + 1) * 100.0 / NoOfSearches;

				if (Math.Abs(ProgressBarValue - 100) > tolerance)
				{ continue; }

				ProgressBarValue = 0;
				ProgressBarVisibility = Visibility.Collapsed;
			}
			var end = DateTime.Now;
			var totalElapsedTime = (end - start).TotalMilliseconds;
			return SimulationResults(totalNoOfIterations, totalElapsedTime);
		}

		private ISimulationResults SimulateBinarySearch(BaseSearch binarySearch)
		{
			TargetValue = binarySearch.ElementAt(0);
			var entries = binarySearch.FindItem(TargetValue);
			TargetIndex = entries.TargetIndex;

			var start = DateTime.Now;
			var totalNoOfIterations = 0.0;
			for (var j = 0; j < NoOfSearches; j++)
			{
				var value = BaseSearch.Random.Next(binarySearch.StartValue, binarySearch.EndValue);
				var searchItem = binarySearch.FindItem(value);
				totalNoOfIterations += searchItem.NoOfIters;
			}
			var end = DateTime.Now;

			var totalElapsedTime = (end - start).TotalMilliseconds;
			return SimulationResults(totalNoOfIterations, totalElapsedTime);
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
				sb.Append(BinarySearchType.ElementAt(i) + ", ");
			}

			return (toIndex == NoOfEntries - 1)
				? sb.Append(BinarySearchType.ElementAt(toIndex))
				: sb.Append(BinarySearchType.ElementAt(toIndex) + ", ");
		}


		/***************************************** Private Fields ******************************************/
		private int _targetValue;
	}
}
