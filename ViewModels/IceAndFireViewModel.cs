using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace icefire.ViewModels
{
    // Base class for all the ViewModels that can navigate to the details page of an entity indentified by an url.
    class IceAndFireViewModel : ViewModelBase
    {
        // Navigates to the specified type of details page and loads the data of the entity (book, character or house) at the given url into it.
        public void NavigateToDetails(System.Type detailsPage, string url)
        {
            if (url != null && !string.IsNullOrWhiteSpace(url))
                NavigationService.Navigate(detailsPage, url);
        }
    }
}
