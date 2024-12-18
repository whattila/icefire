using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Data;

namespace icefire
{
    // sample: https://github.com/Windows-XAML/Template10/blob/master/Templates%20(Project)/Minimal/App.xaml.cs

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        // Constructor; also sets the theme of the app.
        public App()
        {
            InitializeComponent();
            RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Light;
        }

        // Called at the start of the app; sets the MainPage as the current page.
        // At the end of development, it must be set to MainPage.
        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }
    }
}

