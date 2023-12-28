using Client.Static;
using Core.Models;
using System.Net.Http.Json;

namespace Client.Services {
    internal sealed class DatabaseCache {

        private readonly HttpClient _httpClient;
        public DatabaseCache(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        private List<SectionModel> _sections = null;
        internal List<SectionModel> Sections {
            get { return _sections; }
            set {
                _sections = value;
                NotifyCategoriesDataChanges();
            }
        }

        internal async Task<SectionModel> GetCategoryById(int categoryId) {
            if (_sections == null) {
                await GetSectionsFromDbCache();
            }
            return _sections.First(category => category.Id == categoryId);
        }

        private bool _gettingSectionsFromDbCache = false;

        internal async Task GetSectionsFromDbCache() {
            if (_gettingSectionsFromDbCache == false) {
                _gettingSectionsFromDbCache = true;
                _sections = await _httpClient.GetFromJsonAsync<List<SectionModel>>(ApiEndpoints.s_sections);
                _gettingSectionsFromDbCache = false;
            }

        }

        internal event Action OnSectionsDataChanged;

        private void NotifyCategoriesDataChanges() => OnSectionsDataChanged?.Invoke();
    }
}
