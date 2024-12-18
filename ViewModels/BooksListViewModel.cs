using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Template10.Mvvm;
using System.Collections.ObjectModel;
using icefire.Models;
using icefire.Services;

namespace icefire.ViewModels
{
    class BooksListViewModel : ListViewModel
    {
        // The currently loaded page of entities (page size is 10).
        public ObservableCollection<Book> PageOfBooks { get; set; } = new ObservableCollection<Book>();

        // Constructor. Initializes the commands with the functions with the concrete model classes.
        public BooksListViewModel()
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
            LoadFirstPage<Book>(PageOfBooks, category);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadNextPage()
        {
            LoadOtherPage<Book>(PageOfBooks, NextLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadPreviousPage()
        {
            LoadOtherPage<Book>(PageOfBooks, PreviousLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadFirstPage()
        {
            LoadOtherPage<Book>(PageOfBooks, FirstLink);
        }

        // Callback to the DelegateCommand object.
        // It's here because only here we know the concrete model class to call the service with.
        private async void LoadLastPage()
        {
            LoadOtherPage<Book>(PageOfBooks, LastLink);
        }
    }
}
