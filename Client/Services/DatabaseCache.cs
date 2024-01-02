using Client.Static;
using Core.Models;
using System.Net.Http.Json;

namespace Client.Services {
    internal sealed class DatabaseCache {

        private readonly HttpClient _httpClient;
        public DatabaseCache(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        #region Categories


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

        internal async Task<SectionModel> GetCategoryBuName(string categoryName, bool nameToLowerFromUrl) {
            if (_sections == null) {
                await GetSectionsFromDbCache();
            }
            SectionModel categoryToReturn = null;
            if (nameToLowerFromUrl) {
                categoryToReturn = _sections.First(category => category.Title.ToLowerInvariant() == categoryName);
            }
            else {
                categoryToReturn = _sections.First(category => category.Title == categoryName);
            }
            return categoryToReturn;
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

        #endregion


        #region Surveys
        private List<SurveyModel> _surveys = null;
        internal List<SurveyModel> Surveys {
            get { return _surveys; }
            set {
                _surveys = value;
                NotifySurveysDataChanges();
            }
        }


        internal async Task<List<SurveyModel>> GetSurveysByCategoryId(int categoryId) {
            if (_surveys == null) {
                await GetSurveysFromDbCache();
            }

            var surveysByCategoryId = _surveys.FindAll(x => x.SectionId == categoryId);
            if (surveysByCategoryId == null) {
                throw new ArgumentNullException(nameof(surveysByCategoryId));
            }

            return surveysByCategoryId;
        }


        private bool _gettingSurveysFromDbCache = false;


        internal async Task<SurveyModel> GetSurveyById(int surveyId) {
            if (_surveys == null) {
                await GetSurveysFromDbCache();
            }

            var survey = _surveys.First(survey => survey.Id == surveyId);
            if (survey == null) {
                throw new ArgumentNullException(nameof(survey));
            }

            return survey;
        }

        internal async Task<SurveyDTO> GetSurveyDTOById(int surveyId) => await _httpClient.GetFromJsonAsync<SurveyDTO>($"{ApiEndpoints.s_surveysDTO}/{surveyId}");




        internal async Task GetSurveysFromDbCache() {
            if (_gettingSurveysFromDbCache == false) {
                _gettingSurveysFromDbCache = true;
                _surveys = await _httpClient.GetFromJsonAsync<List<SurveyModel>>(ApiEndpoints.s_surveys);
                _gettingSurveysFromDbCache = false;
            }

        }

        internal event Action OnSurveysDataChanged;

        private void NotifySurveysDataChanges() => OnSurveysDataChanged?.Invoke();
        #endregion
    }
}
