using OnSale.Common.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnSale.Prism.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {
        private Country _country;


        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Details";
        }


        public Country Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

    

        //mudar isto
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<Country>("country");
                            
                Title = Country.name;
               
            }
        }
    }
}
