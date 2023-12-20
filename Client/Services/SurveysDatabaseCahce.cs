using Client.Static;
using Core.Models;
using System.Net.Http.Json;

namespace Client.Services {
    internal sealed class SurveysDatabaseCache {

        private readonly HttpClient _httpClient;
        public SurveysDatabaseCache(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        private List<SurveyModel> _surveys = null;
        internal List<SurveyModel> Surveys {
            get { return _surveys; }
            set {
                _surveys = value;
                NotifyCategoriesDataChanges();
            }
        }



        private bool _gettingSurveysFromDbCache = false;

        internal async Task GetSectionsFromDbCache() {
            if (_gettingSurveysFromDbCache == false) {
                _gettingSurveysFromDbCache = true;
                _surveys = await _httpClient.GetFromJsonAsync<List<SurveyModel>>(ApiEndpoints.s_surveys);
                _gettingSurveysFromDbCache = false;
            }

        }

        internal event Action OnSurveysDataChanged;

        private void NotifyCategoriesDataChanges() => OnSurveysDataChanged?.Invoke();
    }
}
