using CompareSearchMethods.Model;
using CompareSearchMethods.Model.Interfaces;
using CompareSearchMethods.ViewModel;

namespace CompareSearchMethods.View
{
	public partial class MainView
	{
		public MainView()
		{
			InitializeComponent();
			ISearchItem searchItem = new SearchItem();
			var vm = new MainViewModel(searchItem);
			DataContext = vm;
		}
	}
}
