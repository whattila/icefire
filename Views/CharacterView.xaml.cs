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
    public sealed partial class CharacterView : Page
    {
        public CharacterView()
        {
            this.InitializeComponent();
        }

        private void Father_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Father");
        }

        private void Mother_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Mother");
        }

        private void Spouse_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GetCharacterRole("Spouse");
        }

        private void Allegiance_ItemClick(object sender, ItemClickEventArgs e)
        {
            var house = (House)e.ClickedItem;
            ViewModel.NavigateToDetails(typeof(Views.HouseView), house.Url);
        }

        private void Book_ItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (Book)e.ClickedItem;
            ViewModel.NavigateToDetails(typeof(Views.BookView), book.Url);
        }
    }
}
