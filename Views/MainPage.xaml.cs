using System;
using Windows.UI.Xaml.Controls;

namespace icefire.Views
{
    public sealed partial class MainPage : Page
    {
        // constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Categories_ItemClick(object sender, ItemClickEventArgs e)
        {
            var category = (string)e.ClickedItem;
            ViewModel.NavigateToCategory(category);
        }
    }
}
