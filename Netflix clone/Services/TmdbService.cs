﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Netflix_clone.Models;


namespace Netflix_clone.Services
{
    public class TmdbService:ITmdbService
    {
        private ILogger<TmdbService> _logger;
        private const  string api_key = "5f99b4adfc7ffd890447acf4a795afde";
        public const string TmdbClientName = "TmdbClient";
        private readonly IHttpClientFactory _httpClientFactory;

        public TmdbService(IHttpClientFactory httpClientFactory,ILogger<TmdbService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        private HttpClient HttpClient => _httpClientFactory.CreateClient(TmdbClientName);

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            var genresWrapper = await HttpClient.GetFromJsonAsync<GenreWrapper>($"{TmdbUrls.MovieGenres}&api_key={api_key}");
               
            return genresWrapper.Genres;
        }
        public async Task<IEnumerable<Media>> GetTrendingAsync() =>
            await GetMediaAsync(TmdbUrls.Trending);
       
        
        public async Task<IEnumerable<Media>> GetTopRatedAsync() =>
            await GetMediaAsync(TmdbUrls.TopRated);

        public async Task<IEnumerable<Media>> GetNetflixOriginals() =>
            await GetMediaAsync(TmdbUrls.NetflixOriginals);
        public async Task<IEnumerable<Media>> GetActionasync() =>
            await GetMediaAsync(TmdbUrls.Action);
        
        public async Task<IEnumerable<Video>?> GetTrailers(int id,string type = "movie")
        {
            var videosWrapper = await HttpClient.GetFromJsonAsync<VideosWrapper>(
                $"{TmdbUrls.GetTrailers(id, type)}&api_key={api_key}");
            
            if(videosWrapper?.results?.Length > 0)
            {
                var trailerTeasers = videosWrapper.results.Where(VideosWrapper.FilterTrailerTeasers);
                var trailerJson = JsonSerializer.Serialize(trailerTeasers);
                _logger.LogInformation($"Trailer Teasers: {trailerJson}");
                return trailerTeasers;
                
            }
            _logger.LogInformation("this is from get trailer it is null::::::");
            return null;
        }

        public async Task<MovieDetail> GetMediaDetails(int id,string type = "movie")
        {
            var details = await HttpClient.GetFromJsonAsync<MovieDetail>(
                $"{TmdbUrls.GetMovieDetails(id, type)}&api_key={api_key}"
                );
            return details;
        }
        public async Task<IEnumerable<Media>> GetSimilarAsync(int id,string type = "movie") =>
           await GetMediaAsync(
               $"{TmdbUrls.GetSimilar(id, type)}&api_key={api_key}");


        private async Task<IEnumerable<Media>> GetMediaAsync(string url)
        {
            try
            {
                var Movies_Collection = await HttpClient.GetFromJsonAsync<Movie>($"{url}&api_key={api_key}");
                _logger.LogInformation($"{Movies_Collection.results.Length}");
                if (Movies_Collection == null)
                {
                    return null;
                }
                return Movies_Collection.results.Select(r => r.ToMediaObject());


            }
            catch (Exception ex)
            {
                _logger.LogInformation($"this is from service class ;{ex.InnerException}");
                return null;
            }


        }

  
    }
    public static class TmdbUrls
    {
        public const string Trending = "3/trending/all/week?language=en-US";
        public const string NetflixOriginals = "3/discover/tv?language=en-US&with_networks=213";
        public const string TopRated = "3/movie/top_rated?language=en-US";
        public const string Action = "3/discover/movie?language=en-US&with_genres=28";
        public const string MovieGenres = "3/genre/movie/list?language=en-US";



        public static string GetTrailers(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/videos?language=en-US";
        public static string GetMovieDetails(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}?language=en-US";
        public static string GetSimilar(int movieId, string type = "movie") => $"3/{type ?? "movie"}/{movieId}/similar?language=en-US";
    }
    public class Movie
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result
    {
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; } // "movie" or "tv"
        public string ThumbnailPath => poster_path ?? backdrop_path;
        public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2/{ThumbnailPath}";
        public string ThumbnailSmall => $"https://image.tmdb.org/t/p/w220_and_h330_face/{ThumbnailPath}";
        public string ThumbnailUrl => $"https://image.tmdb.org/t/p/original/{ThumbnailPath}";
        public string DisplayTitle => title ?? name ?? original_title ?? original_name;

        public Media ToMediaObject()
        {
            return new ()
            {
                Id = id,
                DisplayTitle = DisplayTitle,
                Media_Type = media_type,
                Overview = overview,
                Release_date = release_date,
                Thumbnail = Thumbnail,
                ThumbnailSmall = ThumbnailSmall,
                ThumbnailUrl = ThumbnailUrl,
                


            };
        }
       
    }


    public class VideosWrapper
    {
        public int id { get; set; }
        public Video[] results { get; set; }

        public static Func<Video, bool> FilterTrailerTeasers => v =>
            v.official
            && v.site.Equals("Youtube", StringComparison.OrdinalIgnoreCase)
            && (v.type.Equals("Teaser", StringComparison.OrdinalIgnoreCase) || v.type.Equals("Trailer", StringComparison.OrdinalIgnoreCase));
    }

    public class Video
    {
        public string name { get; set; }
        public string key { get; set; }
        public string site { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
        public DateTime published_at { get; set; }
        public string Thumbnail => $"https://i.ytimg.com/vi/{key}/mqdefault.jpg";
    }


    public class MovieDetail
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        //public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }
    public class GenreWrapper
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
    public record struct Genre(int Id, string Name);
}
