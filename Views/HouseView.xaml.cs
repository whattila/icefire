using icefire.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace icefire.Views
{
    public sealed partial class HouseView : Page
    {
        public HouseView()
        {
            this.InitializeComponent();
        }

        private void Currentlord_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Currentlord");
        }

        private void Heir_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Heir");
        }

        private void Overlord_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigateToOverlord();
        }

        private void Founder_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Founder");
        }

        private void Cadet_ItemClick(object sender, ItemClickEventArgs e)
        {
            var cadet = (House)e.ClickedItem;
            ViewModel.NavigateToDetails(typeof(HouseView), cadet.Url);
        }

        private void Member_ItemClick(object sender, ItemClickEventArgs e)
        {
            var member = (Character)e.ClickedItem;
            ViewModel.NavigateToDetails(typeof(CharacterView), member.Url);
        }
    }
}
