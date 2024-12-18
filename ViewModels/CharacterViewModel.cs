using icefire.Models;
using icefire.Services;
using icefire.Views;
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
    class CharacterViewModel : IceAndFireViewModel
    {
        private Character _character;

        // The book that we have info loaded about on the current page.
        public Character Character
        {
            get { return _character; }
            set { Set(ref _character, value); }
        }

        // The father of the current character.
        private Character _father;
        public Character Father
        {
            get { return _father; }
            set { Set(ref _father, value); }
        }

        // The mother of the current character.
        private Character _mother;
        public Character Mother
        {
            get { return _mother; }
            set { Set(ref _mother, value); }
        }

        // The spouse of the current character.
        private Character _spouse;
        public Character Spouse
        {
            get { return _spouse; }
            set { Set(ref _spouse, value); }
        }

        // The list of houses that the current character has allegiance to.
        public ObservableCollection<House> Allegiances { get; set; } = new ObservableCollection<House>();

        // The list of books that the current character appears in.
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        // The list of books that the current character has a pov-chapter in.
        public ObservableCollection<Book> Povs { get; set; } = new ObservableCollection<Book>();

        // Called when we navigate to this page. Overrides ViewModelBase.OnNavigatedToAsync.
        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var url = (string)parameter;
            var service = new IceAndFireService();
            Character = await service.GetSpecificEntity<Character>(url);

            // After we have gotten the character entity, we get all the entities found above as well and store them in the respective properties and collections,
            // because that is the only way to show their names.
            if (Character.Father != null && !string.IsNullOrWhiteSpace(Character.Father))
                Father = await service.GetSpecificEntity<Character>(Character.Father);
            if (Character.Mother != null && !string.IsNullOrWhiteSpace(Character.Mother))
                Mother = await service.GetSpecificEntity<Character>(Character.Mother);
            if (Character.Spouse != null && !string.IsNullOrWhiteSpace(Character.Spouse))
                Spouse = await service.GetSpecificEntity<Character>(Character.Spouse);

            foreach (var houseUrl in Character.Allegiances)
            {
                var house = await service.GetSpecificEntity<House>(houseUrl);
                Allegiances.Add(house);
            }

            foreach (var bookUrl in Character.Books)
            {
                var book = await service.GetSpecificEntity<Book>(bookUrl);
                Books.Add(book);
            }

            foreach (var bookUrl in Character.PovBooks)
            {
                var book = await service.GetSpecificEntity<Book>(bookUrl);
                Povs.Add(book);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        // Used when we navigate to the page of another character related to this character.
        // We specify the relation type, get the url of the appropriate character and navigate to the requested page. 
        public void GetCharacterRole(string role)
        {
            role = role.ToLower();
            string url = "";
            switch (role)
            {
                case "father": { url = Father.Url; break; }
                case "mother": { url = Mother.Url; break; }
                case "spouse": { url = Spouse.Url; break; }
            }
            NavigateToDetails(typeof(CharacterView), url);
        }
    }
}
