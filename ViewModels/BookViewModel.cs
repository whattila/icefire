using icefire.Models;
using icefire.Services;
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
    class BookViewModel : IceAndFireViewModel
    {
        private Book _book;

        // The book that we have info loaded about on the current page.
        public Book Book
        {
            get { return _book; }
            set { Set(ref _book, value); }
        }

        // The list of characters in the current book.
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        // The list of characters in the current book who have a point-of-view (POV) chapter.
        public ObservableCollection<Character> Povs { get; set; } = new ObservableCollection<Character>();

        // Called when we navigate to this page. Overrides ViewModelBase.OnNavigatedToAsync.
        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var url = (string)parameter;
            var service = new IceAndFireService();
            Book = await service.GetSpecificEntity<Book>(url);

            // After we have gotten the book, we get the entities of each character and pov-character as well and store them in the respective collections,
            // because that is the only way to show their names.
            foreach(var characterUrl in Book.Characters)
            {
                var character = await service.GetSpecificEntity<Character>(characterUrl);
                Characters.Add(character);
            }
            foreach (var characterUrl in Book.PovCharacters)
            {
                var character = await service.GetSpecificEntity<Character>(characterUrl);
                Povs.Add(character);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
