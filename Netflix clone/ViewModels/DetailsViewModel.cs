using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Netflix_clone.Models;
using Netflix_clone.Services;

namespace Netflix_clone.ViewModels
{
    [QueryProperty(nameof(Media), "Media")]
    public partial class DetailsViewModel :ObservableObject
    {
        private readonly ITmdbService _mdbService;
        public DetailsViewModel(ITmdbService tmdbService)
        {
            _mdbService = tmdbService;
            
        }
        [ObservableProperty]
        private Media _media;

        [ObservableProperty]
        private string mainTrailerUrl;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private int runtime;

        public async Task InitializeAsync()
        {
            isBusy = true;
            try
            {
                var trailersTreasers = await _mdbService.GetTrailers(Media.Id, Media.Media_Type);
                var details = await _mdbService.GetMediaDetails(Media.Id, Media.Media_Type);
                if (trailersTreasers?.Any() == true)
                {
                    var trailer = trailersTreasers.FirstOrDefault(tr => tr.type == "Trailer");
                    if (trailer is null)
                    {
                        trailer = trailersTreasers.FirstOrDefault();
                    }
                    MainTrailerUrl = GenerateYoutubeUrl(trailer.key);

                    
                   
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not found", "No videos found", "Ok");
                }
                if (details is not null)
                {
                    Runtime = details.runtime;
                }


            }
            finally { isBusy = false; }
        }
        private static string GenerateYoutubeUrl(string videoKey) =>
            $"https://www.youtube.com/embed/{videoKey}";
    }
}
