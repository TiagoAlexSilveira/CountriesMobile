using OnSale.Common.Entities;
using OnSale.Common.Helpers;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnSale.Prism.ItemViewModels
{
    public class CountryItemViewModel : Country
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;
        public ObservableCollection<Border> Borders { get; set; }
        

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Borders = new ObservableCollection<Border>();
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };

            this.GetBorders(this);
            
            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }


        private void GetBorders(CountryItemViewModel model)
        {
            foreach (var item in model.borders)
            {
                foreach (var item2 in CountryGlobal.CountryGlobalList)
                {
                    if (item == item2.alpha3Code)
                    {
                        Border boorder = new Border
                        {
                            BorderFlag = item2.flag,
                            BorderName = item2.name
                        };

                        Borders.Add(boorder);
                    }
                }
            }
        }

    }
}
