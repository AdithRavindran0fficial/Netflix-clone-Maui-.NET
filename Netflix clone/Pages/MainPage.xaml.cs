using Netflix_clone.Services;

namespace Netflix_clone.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly ITmdbService _tmdbService;

        public MainPage(ITmdbService tmdbServices)
        {
            InitializeComponent();
            _tmdbService = tmdbServices;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var trendingData = await _tmdbService.GetTrendingAsync();
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
