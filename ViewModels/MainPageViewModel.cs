using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using icefire.Views;

namespace icefire.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        // The categories of entities available from the API this app is built on.
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string> { "Books", "Houses", "Characters" };

        // Called by the Click event handler of the items in the list on the page.
        // We get the clicked category as parameter, then load the appropriate page.
        public void NavigateToCategory(string category)
        {
            category = category.ToLower();
            System.Type pageToLoad = typeof(MainPage);
            switch (category)
            {
                case "books": { pageToLoad = typeof(BooksList); break; }
                case "houses": { pageToLoad = typeof(HousesList); break; }
                case "characters": { pageToLoad = typeof(CharactersList); break; }
            }
            NavigationService.Navigate(pageToLoad, category);
        }
    }
}
