using recipebook.blazor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.Models
{
    public class AppState
    {
        public List<Recipe> SearchResults { get; set; } = new List<Recipe>();
    }
}
