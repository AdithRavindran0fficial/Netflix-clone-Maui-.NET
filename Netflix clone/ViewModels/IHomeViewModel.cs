﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Netflix_clone.Models;

namespace Netflix_clone.ViewModels
{
    public  interface IHomeViewModel
    {
        Media TrendingMovie { get; set; }
        ObservableCollection<Media> Trending   { get; }
        ObservableCollection<Media> TopRated { get; }
        ObservableCollection<Media> NetflixOriginals { get; }
        ObservableCollection<Media> ActionMovie { get; }
        
        void SelectMedia(Media? media = null);
        

        Task InitializingMoviesAsync();
    }
}
