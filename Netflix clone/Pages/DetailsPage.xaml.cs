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
        try
        {
            base.OnAppearing();
            await DetailsViewModel.InitializeAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
       
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > 0)
        {
            DetailsViewModel.SimilarItemWidth = Convert.ToInt32(width / 3)-3;
        }
    }

    private void TrailerTab_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            similarTabIndiactor.Color = Colors.Black;
            similarTabContent.IsVisible = false;

            trailersTabContent.IsVisible = true;
            trailersTabIndicator.Color = Colors.Red;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in TrailerTab_Tapped: {ex.Message}");
        }
    }


    private void SimilarTab_Tapped(object sender, TappedEventArgs e)
    {
        
        
        trailersTabIndicator.Color = Colors.Black;
        trailersTabContent.IsVisible = false;

        similarTabIndiactor.Color = Colors.Red;
        similarTabContent.IsVisible = true;


    }
}