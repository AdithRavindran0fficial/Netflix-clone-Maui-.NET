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
}