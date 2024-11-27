using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Netflix_clone.Models;
using Netflix_clone.Services;

namespace Netflix_clone.ViewModels
{
    public partial class HomeViewModel:ObservableObject,IHomeViewModel
    {
        private readonly ITmdbService _tmdbService;
        public HomeViewModel(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
            
        }
        [ObservableProperty]
        public Media trendingMovie;
        public ObservableCollection<Media> Trending { get; set; } = new();
        public ObservableCollection<Media> TopRated { get; set; } = new();
        public ObservableCollection<Media> NetflixOriginals { get; set; } = new();
        public ObservableCollection<Media> ActionMovie { get; set; } = new();


        public async Task InitializingMoviesAsync()
        {
            var trendingTask =  _tmdbService.GetTrendingAsync();      
            var TopratedMovieTask =  _tmdbService.GetTopRatedAsync();  
            var NetflixOriginMoviesTask = _tmdbService.GetNetflixOriginals();   
            var ActionMoviesTask =   _tmdbService.GetActionasync();

            var movieCollection = await Task.WhenAll(trendingTask, TopratedMovieTask, NetflixOriginMoviesTask,ActionMoviesTask);

            var TrendingCollection = movieCollection[0];
            var TopRatedCollection = movieCollection[1];
            var NetflixOriginMovieCollection = movieCollection[2];
            var ActionMovieCollection = movieCollection[3];

            TrendingMovie = TrendingCollection.OrderBy(t => Guid.NewGuid()).
                FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.DisplayTitle) 
                && !string.IsNullOrWhiteSpace(t.Thumbnail)); 

            Inializer(TrendingCollection, Trending);
            Inializer(TopRatedCollection, TopRated);
            Inializer(ActionMovieCollection, ActionMovie);
            Inializer(NetflixOriginMovieCollection, NetflixOriginals);
            
        }
        private void Inializer(IEnumerable<Media> media ,ObservableCollection<Media> collection)
        {
            collection.Clear();
            foreach(var movies in media)
            {
                collection.Add(movies);
            }
        }
        
    }
}
