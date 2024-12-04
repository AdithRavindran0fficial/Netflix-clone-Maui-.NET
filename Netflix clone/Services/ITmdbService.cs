using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix_clone.Models;

namespace Netflix_clone.Services
{
    public  interface ITmdbService
    {
        Task<IEnumerable<Media>> GetTrendingAsync();
        Task<IEnumerable<Media>> GetTopRatedAsync();

        
        Task<IEnumerable<Media>> GetNetflixOriginals();
        Task<IEnumerable<Media>> GetActionasync();

        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<IEnumerable<Video>?> GetTrailers(int id, string type = "movie");
        Task<MovieDetail> GetMediaDetails(int id, string type = "movie");
    }
}
