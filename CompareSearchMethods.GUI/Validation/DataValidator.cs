using CompareSearchMethods.GUI.ViewModel;
using CompareSearchMethods.Model.Properties;
using FluentValidation;

namespace CompareSearchMethods.GUI.Validation
{
	public class DataValidator : AbstractValidator<MainViewModel>
	{
		public DataValidator()
		{
			ValidationRules();
		}

		#region PrivateMethods

		private void ValidationRules()
		{
			// No. of Entries
			RuleFor(vm => vm.NoOfEntries)
				.NotEmpty()
				.WithMessage(vm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(vm.NoOfEntries)))
				.Must(noOfEntries => int.TryParse(noOfEntries.ToString(), out int _))
				.WithMessage(vm => string.Format(Resources.InvalidIntegerError, nameof(vm.NoOfEntries)))
				.Must(noOfEntries => int.Parse(noOfEntries.ToString()) >= Settings.Default.MinNoOfEntries)
				.WithMessage(vm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(vm.NoOfEntries), Settings.Default.MinNoOfEntries))
				.Must(noOfEntries => int.Parse(noOfEntries.ToString()) <= Settings.Default.MaxNoOfEntries)
				.WithMessage(vm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(vm.NoOfEntries), Settings.Default.MaxNoOfEntries));

			// No. of Searches
			RuleFor(sm => sm.NoOfEntries)
				.NotEmpty()
				.WithMessage(vm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(vm.NoOfSearches)))
				.Must(noOfSearches => int.TryParse(noOfSearches.ToString(), out int _))
				.WithMessage(vm => string.Format(Resources.InvalidIntegerError, nameof(vm.NoOfSearches)))
				.Must(noOfSearches => int.Parse(noOfSearches.ToString()) >= Settings.Default.MinNoOfSearches)
				.WithMessage(vm => string.Format(Resources.NoOfSearchesTooSmallError, nameof(vm.NoOfSearches), Settings.Default.MinNoOfSearches))
				.Must(noOfSearches => int.Parse(noOfSearches.ToString()) <= Settings.Default.MaxNoOfSearches)
				.WithMessage(vm => string.Format(Resources.NoOfSearchesTooLargeError, nameof(vm.NoOfSearches), Settings.Default.MaxNoOfSearches));

			#endregion PrivateMethods
		}
	}
}