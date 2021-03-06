﻿using SearchMethods.Model.Properties;
using SearchMethods.GUI.ViewModel;
using FluentValidation;

namespace SearchMethods.GUI
{
    public class InputValidation : AbstractValidator<MainViewModel>
    {
        public InputValidation() => ValidationRules();

        private void ValidationRules()
        {
            // No. of Entries
            RuleFor(sm => sm.NoOfEntries)
                .NotEmpty()
                .WithMessage(sm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(sm.NoOfEntries)))
                .Must(noOfEntries => int.TryParse(noOfEntries.ToString(), out int _))
                .WithMessage(sm => string.Format(Resources.InvalidIntegerError, nameof(sm.NoOfEntries)))
                .Must(noOfEntries => int.Parse(noOfEntries.ToString()) >= Settings.Default.MinNoOfEntries)
                .WithMessage(sm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(sm.NoOfEntries), Settings.Default.MinNoOfEntries))
                .Must(noOfEntries => int.Parse(noOfEntries.ToString()) <= Settings.Default.MaxNoOfEntries)
                .WithMessage(sm => string.Format(Resources.NoOfEntriesTooSmallError, nameof(sm.NoOfEntries), Settings.Default.MaxNoOfEntries));

            // No. of Searches
            RuleFor(sm => sm.NoOfSearches)
                .NotEmpty()
                .WithMessage(sm => string.Format(Resources.ValueNullOrWhiteSpaceError, nameof(sm.NoOfSearches)))
                .Must(noOfSearches => int.TryParse(noOfSearches.ToString(), out int _))
                .WithMessage(sm => string.Format(Resources.InvalidIntegerError, nameof(sm.NoOfSearches)))
                .Must(noOfSearches => int.Parse(noOfSearches.ToString()) >= Settings.Default.MinNoOfSearches)
                .WithMessage(sm => string.Format(Resources.NoOfSearchesTooSmallError, nameof(sm.NoOfSearches), Settings.Default.MinNoOfSearches))
                .Must(noOfSearches => int.Parse(noOfSearches.ToString()) <= Settings.Default.MaxNoOfSearches)
                .WithMessage(sm => string.Format(Resources.NoOfSearchesTooLargeError, nameof(sm.NoOfSearches), Settings.Default.MaxNoOfSearches));
        }
    }
}
