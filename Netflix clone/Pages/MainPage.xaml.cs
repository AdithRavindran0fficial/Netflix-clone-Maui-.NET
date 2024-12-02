using Netflix_clone.Services;
using Netflix_clone.ViewModels;

namespace Netflix_clone.Pages
{
    public partial class MainPage : ContentPage
    {
        
        private readonly IHomeViewModel _homeViewModel;

        public MainPage(IHomeViewModel viewmodel)
        {
            InitializeComponent();
            _homeViewModel = viewmodel;
            BindingContext = _homeViewModel;
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _homeViewModel.InitializingMoviesAsync();
        }

        private void MovieRow_MediaSelected(object sender, Controls.MediaSelectEventArgs e)
        {
            _homeViewModel.SelectMedia(e.Media);
        }

        private void MovieInfoBox_InfoBoxClosed(object sender, EventArgs e)
        {
            _homeViewModel.SelectMedia(null);
        }

        private async void Category_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CategoriesPage));

        }
    }

}
