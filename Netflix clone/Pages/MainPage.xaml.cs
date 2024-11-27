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
        
    }

}
