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
    class HouseViewModel : IceAndFireViewModel
    {
        private House _house, _overlord;

        // The house entity currently loaded.
        public House House
        {
            get { return _house; }
            set { Set(ref _house, value); }
        }

        // The overlord house of the currently loaded house.
        public House Overlord
        {
            get { return _overlord; }
            set { Set(ref _overlord, value); }
        }

        private Character _currentLord, _heir, _founder;

        // The current lord, the heir, and the founder characters of the currently loaded house.
        public Character CurrentLord
        {
            get { return _currentLord; }
            set { Set(ref _currentLord, value); }
        }

        public Character Heir
        {
            get { return _heir; }
            set { Set(ref _heir, value); }
        }

        public Character Founder
        {
            get { return _founder; }
            set { Set(ref _founder, value); }
        }

        // The cadet houses of the current house.
        public ObservableCollection<House> CadetBranches { get; set; } = new ObservableCollection<House>();

        // Characters sworn to this house.
        public ObservableCollection<Character> SwornMembers { get; set; } = new ObservableCollection<Character>();

        // Called when we navigate to this page. Overrides ViewModelBase.OnNavigatedToAsync.
        public override async Task OnNavigatedToAsync(
            object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var url = (string)parameter;
            var service = new IceAndFireService();
            House = await service.GetSpecificEntity<House>(url);

            // After we have gotten the house entity, we get all the entities found above as well and store them in the respective properties and collections,
            // because that is the only way to show their names.
            if (House.CurrentLord != null && !string.IsNullOrWhiteSpace(House.CurrentLord))
                CurrentLord = await service.GetSpecificEntity<Character>(House.CurrentLord);

            if (House.Heir != null && !string.IsNullOrWhiteSpace(House.Heir))
                Heir = await service.GetSpecificEntity<Character>(House.Heir);

            if (House.Overlord != null && !string.IsNullOrWhiteSpace(House.Overlord))
                Overlord = await service.GetSpecificEntity<House>(House.Overlord);

            if (House.Founder != null && !string.IsNullOrWhiteSpace(House.Founder))
                Founder = await service.GetSpecificEntity<Character>(House.Founder);

            foreach (var houseUrl in House.CadetBranches)
            {
                var house = await service.GetSpecificEntity<House>(houseUrl);
                CadetBranches.Add(house);
            }

            foreach (var characterUrl in House.SwornMembers)
            {
                var character = await service.GetSpecificEntity<Character>(characterUrl);
                SwornMembers.Add(character);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        // Used when we navigate to the page of a character related to this house.
        // We specify the relation type, get the url of the appropriate character and navigate to the requested page. 
        public void GetCharacterRole(string role)
        {
            role = role.ToLower();
            string url = "";
            switch (role)
            {
                case "currentlord": { url = CurrentLord.Url; break; }
                case "heir": { url = Heir.Url; break; }
                case "founder": { url = Founder.Url; break; }
            }
            NavigateToDetails(typeof(CharacterView), url);
        }

        // Navigates to the house page of the overlord house of this house
        public void NavigateToOverlord()
        {
            NavigateToDetails(typeof(HouseView), Overlord.Url);
        }
    }
}
