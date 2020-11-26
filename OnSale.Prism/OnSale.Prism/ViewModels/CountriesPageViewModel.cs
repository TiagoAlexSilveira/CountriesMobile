using OnSale.Common.Entities;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using OnSale.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnSale.Prism.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        //private ObservableCollection<Country> _country;
        private bool _isRunning;

        private string _search;
        private List<Country> _myCountries;
        private DelegateCommand _searchCommand;

        private ObservableCollection<CountryItemViewModel> _countries;



        public CountriesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Countries";
            _navigationService = navigationService;
            _apiService = apiService;
            LoadCountriesAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }


        //public ObservableCollection<Country> Countries
        //{
        //    get => _country;
        //    set => SetProperty(ref _country, value);
        //}

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void LoadCountriesAsync()
        {
            //if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.",
            //   "Accept");
            //    return;

            //}

            IsRunning = true;

            Response response = await _apiService.GetCountries("http://restcountries.eu", "/rest/v2/all");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                "Error",
                response.Message,
                "Accept");
                return;
            }
            //List<Country> myCountries = (List<Country>)response.Result;
            //Countries = new ObservableCollection<Country>(myCountries);
            _myCountries = (List<Country>)response.Result;
            ShowCountries();
        }


        //private void ShowCountries()
        //{
        //    if (string.IsNullOrEmpty(Search))
        //    {
        //        Countries = new ObservableCollection<Country>(_myCountries);
        //    }
        //    else
        //    {
        //        Countries = new ObservableCollection<Country>(_myCountries
        //            .Where(p => p.name.ToLower().Contains(Search.ToLower())));
        //    }

        //}


        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    name = p.name,
                    capital = p.capital,
                    flag = p.flag,

                })
                     .ToList());

            }
            else
            {

                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    name = p.name,
                    capital = p.capital,
                    flag = p.flag,

                })
                    .Where(p => p.name.ToLower().Contains(Search.ToLower()))
                    .ToList());
                
            }
        }
    }
}
