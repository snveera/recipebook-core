using System;
using System.Collections.Generic;

namespace recipebook.blazor.Models
{
    public class AppState
    {
        public event Action OnChange;

        private List<Recipe> _searchResults = null;
        public List<Recipe> SearchResults
        {
            get { return _searchResults; }
            set { _searchResults = value; NotifyStateChanged(); }
        }

        public string SearchError { get; set; }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
