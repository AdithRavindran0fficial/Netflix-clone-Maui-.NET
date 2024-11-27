using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Netflix_clone.Services;
using Netflix_clone.ViewModels;
using Netflix_clone.Pages;

namespace Netflix_clone
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("poppins-Regular.ttf", "PopinsRegular");
                    fonts.AddFont("poppins-Semibold.ttf", "PopinsSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient(TmdbService.TmdbClientName,
                HttpClient => HttpClient.BaseAddress = new Uri(" http://api.themoviedb.org/"));
            builder.Services.AddSingleton<ITmdbService,TmdbService>();
            builder.Services.AddSingleton<IHomeViewModel,HomeViewModel>();
            builder.Services.AddSingleton<MainPage>();
            return builder.Build();
        }
    }
}
