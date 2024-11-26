using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_clone.Models
{
    public class Media
    {
        public string DisplayTitle { get; set; }
        public string Media_Type { get; set; } // "movie" or "tv"

        public string Thumbnail {  get; set; }
        public string ThumbnailSmall { get; set; }
        public string ThumbnailUrl {  get; set; }

        public string Overview { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }

        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int Id { get; set; }
        public string original_title { get; set; }
        public string original_name { get; set; }
      
        public string title { get; set; }
        public string name { get; set; }
        public bool video { get; set; }
        
        //public string ThumbnailPath => poster_path ?? backdrop_path;
        
        

    }
}
