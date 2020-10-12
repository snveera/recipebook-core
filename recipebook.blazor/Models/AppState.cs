using System;
using System.Collections.Generic;

namespace recipebook.blazor.Models
{
    public class AppState
    {
        public event Action OnChange;

        private List<RecipeViewModel> _searchResults = null;
        public List<RecipeViewModel> SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; NotifyStateChanged(); }
        }

        public string SearchError { get; set; }

        public void Clear()
        {
            SearchResults = null;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
