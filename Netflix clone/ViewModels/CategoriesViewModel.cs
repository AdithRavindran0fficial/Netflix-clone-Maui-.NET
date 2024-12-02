using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix_clone.Services;

namespace Netflix_clone.ViewModels
{
    public partial class CategoriesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ITmdbService _tmdb;
        private IEnumerable<Genre> _genreList;

        public ObservableCollection<string> Categories { get; set; }
        public CategoriesViewModel(ITmdbService tmdbService)
        {
            Categories = new ObservableCollection<string>
                
                {
                    "My List",
                    "Downloads"
            };

            _tmdb = tmdbService;
            
        }

        public async Task InitialzeAsync()
        {
            if(_genreList is null)
            {
                _genreList = await _tmdb.GetGenresAsync();
            }
            Categories.Clear();
            Categories.Add("My List");
            Categories.Add("Downloads");
            foreach(var item in _genreList)
            {
                Categories.Add(item.Name);
            }
        }
    }
}
