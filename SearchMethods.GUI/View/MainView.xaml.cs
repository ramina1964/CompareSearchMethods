using SearchMethods.Model;
using SearchMethods.GUI.ViewModel;

namespace SearchMethods.GUI.View
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
