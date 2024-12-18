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
    class CharactersListViewmodel : ListViewModel
    {
        // The currently loaded page of entities (page size is 10).
        public ObservableCollection<Character> PageOfCharacters { get; set; } = new ObservableCollection<Character>();

        // Constructor. Initializes the commands with the functions with the concrete model classes.
        public CharactersListViewmodel()
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
            LoadFirstPage<Character>(PageOfCharacters, category);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadNextPage()
        {
            LoadOtherPage<Character>(PageOfCharacters, NextLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadPreviousPage()
        {
            LoadOtherPage<Character>(PageOfCharacters, PreviousLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadFirstPage()
        {
            LoadOtherPage<Character>(PageOfCharacters, FirstLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadLastPage()
        {
            LoadOtherPage<Character>(PageOfCharacters, LastLink);
        }
    }
}
