using OnSale.Common.Entities;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnSale.Prism.ItemViewModels
{
    public class CountryItemViewModel : Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };


            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }

    }
}
