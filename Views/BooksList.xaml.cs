using icefire.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace icefire.Views
{
    public sealed partial class BooksList : Page
    {
        public BooksList()
        {
            InitializeComponent();
        }

        private void First_OnItemClick(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }

        private void Previous_OnItemClick(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }

        private void Next_OnItemClick(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }

        private void Last_OnItemClick(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }

        private void Books_ItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (Book)e.ClickedItem;
            ViewModel.NavigateToDetails(typeof(Views.BookView), book.Url);
        }
    }
}
