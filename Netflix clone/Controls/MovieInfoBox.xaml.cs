using System.Windows.Input;
using Netflix_clone.Models;

namespace Netflix_clone.Controls;

public partial class MovieInfoBox : ContentView
{
	public static BindableProperty MediaProperty=
		BindableProperty.Create(nameof(Media),typeof(Media),typeof(MovieInfoBox),null);
	public event EventHandler InfoBoxClosed;
	public MovieInfoBox()
	{
		InitializeComponent();
		ClosedCommand = new Command(ExecuteClosedCommand);
	}
	public Media Media
	{
		get =>(Media) GetValue(MovieInfoBox.MediaProperty);
		set => SetValue(MovieInfoBox.MediaProperty, value);
	}
    public ICommand ClosedCommand { get; private set; }
    private void ExecuteClosedCommand()
    {
		
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        InfoBoxClosed.Invoke(this, EventArgs.Empty);
    }
}