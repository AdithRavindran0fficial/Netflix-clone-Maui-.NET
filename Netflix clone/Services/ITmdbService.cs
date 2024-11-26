using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix_clone.Services
{
    public  interface ITmdbService
    {
        Task<IEnumerable<Result>> GetTrendingAsync();
    }
}
