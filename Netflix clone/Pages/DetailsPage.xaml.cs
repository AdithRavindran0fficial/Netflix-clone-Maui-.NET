using Netflix_clone.ViewModels;

namespace Netflix_clone.Pages;

public partial class DetailsPage : ContentPage
{
	DetailsViewModel DetailsViewModel;
	public DetailsPage(DetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		DetailsViewModel = vm;

	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await DetailsViewModel.InitializeAsync();
    }

    private void TrailerTab_Tapped(object sender, TappedEventArgs e)
    {
       similartrailersTabIndiactor.IsVisible = false;
        similartrailersTabIndiactor.Color = Colors.Black;   
        
        trailersTabIndicator.IsVisible = false;
        trailersTabIndicator.Color = Colors.Red;

    }

    private void SimilarTab_Tapped(object sender, TappedEventArgs e)
    {
        similartrailersTabIndiactor.IsVisible = true;
        similartrailersTabIndiactor.Color = Colors.Red;

        trailersTabIndicator.IsVisible = false;
        trailersTabIndicator.Color = Colors.Black;

    }
}