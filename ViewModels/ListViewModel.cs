using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using icefire.Services;
using System.Collections.ObjectModel;

namespace icefire.ViewModels
{
    // The base class for the entity (Book, Character, House) listing pages.
    abstract class ListViewModel : IceAndFireViewModel
    {
        private string previousLink;
        private string nextLink;

        // The links to the first, previous, next and last page of results.
        public string PreviousLink
        {
            get { return previousLink; }
            set { previousLink = value; PreviousPageCommand.RaiseCanExecuteChanged(); }
        }

        public string NextLink
        {
            get { return nextLink; }
            set { nextLink = value; NextPageCommand.RaiseCanExecuteChanged(); }
        }

        public string FirstLink { get; set; }
        public string LastLink { get; set; }

        // The commands for loading the next and previous page of items.
        // The NextPageCommand and PreviousPageCommand cannot be executed if the specific link strings are null.
        public DelegateCommand FirstPageCommand { get; set; }
        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PreviousPageCommand { get; set; }
        public DelegateCommand LastPageCommand { get; set; }

        // Gets the first page of entities of the given category from the service.
        // The T template parameter and the category is given by the derived class.
        public async void LoadFirstPage<T>(ObservableCollection<T> list, string category)
        {
            var service = new IceAndFireService();
            var result = await service.GetFirstGroupPageAsync<T>(category);
            ProcessRequestResult(list, result);
        }

        // Gets the entity page at the given url from the service.
        // The T template parameter is given by the derived class.
        public async void LoadOtherPage<T>(ObservableCollection<T> list, string url)
        {
            if (url != null)
            {
                var service = new IceAndFireService();
                var result = await service.GetGroupPageAsync<T>(url);
                ProcessRequestResult(list, result);
            }
        }

        // Fills the ObservableCollection of entities at the page
        // and sets the links to the next and previous page of results
        // from the Result object gotten from the service.
        private void ProcessRequestResult<T>(ObservableCollection<T> list, Result<List<T>> result)
        {
            list.Clear();
            foreach (var item in result.Entity)
                list.Add(item);
            FirstLink = result.FirstLink;
            PreviousLink = result.PrevLink;
            NextLink = result.NextLink;
            LastLink = result.LastLink;
        }

        // Checks if we can load the previous page of items.
        public bool CanLoadPrevious()
        {
            return PreviousLink != null;
        }

        // Checks if we can load the next page of items.
        public bool CanLoadNext()
        {
            return NextLink != null;
        }

        // Checks if we can load the first page of items.
        // We always can.
        public bool CanLoadFirst()
        {
            return true;
        }

        // Checks if we can load the last page of items.
        // We always can.
        public bool CanLoadLast()
        {
            return true;
        }
    }
}
