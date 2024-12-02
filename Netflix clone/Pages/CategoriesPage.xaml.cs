using Netflix_clone.ViewModels;

namespace Netflix_clone.Pages;

public partial class CategoriesPage : ContentPage
{
	private readonly CategoriesViewModel categoriesViewModel;
	public CategoriesPage(CategoriesViewModel vm)
	{
		InitializeComponent();
		categoriesViewModel = vm;
        BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await categoriesViewModel.InitialzeAsync();
    }

    private async void Closed_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}