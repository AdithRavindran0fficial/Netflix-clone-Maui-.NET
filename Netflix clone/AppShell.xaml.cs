using Netflix_clone.Pages;

namespace Netflix_clone
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        }
    }
}
