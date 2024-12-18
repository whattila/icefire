using icefire.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace icefire.ViewModels
{
    class HousesListViewModel : ListViewModel
    {
        // The currently loaded page of entities (page size is 10).
        public ObservableCollection<House> PageOfHouses { get; set; } = new ObservableCollection<House>();

        // Constructor. Initializes the commands with the functions with the concrete model classes.
        public HousesListViewModel()
        {
            FirstPageCommand = new DelegateCommand(LoadFirstPage, CanLoadFirst);
            NextPageCommand = new DelegateCommand(LoadNextPage, CanLoadNext);
            PreviousPageCommand = new DelegateCommand(LoadPreviousPage, CanLoadPrevious);
            LastPageCommand = new DelegateCommand(LoadLastPage, CanLoadLast);
        }

        // Called when we navigate to this page. Overrides ListViewModel.OnNavigatedToAsync.
        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var category = (string)parameter;
            LoadFirstPage<House>(PageOfHouses, category);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadNextPage()
        {
            LoadOtherPage<House>(PageOfHouses, NextLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadPreviousPage()
        {
            LoadOtherPage<House>(PageOfHouses, PreviousLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadFirstPage()
        {
            LoadOtherPage<House>(PageOfHouses, FirstLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadLastPage()
        {
            LoadOtherPage<House>(PageOfHouses, LastLink);
        }
    }
}

