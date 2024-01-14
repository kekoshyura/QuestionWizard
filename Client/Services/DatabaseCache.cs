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


        #region Questions
        private List<QuestionModel> _questions = null;
        internal List<QuestionModel> Questions {
            get { return _questions; }
            set {
                _questions = value;
                NotifyQuestionsDataChanges();
            }
        }


        internal async Task<List<QuestionModel>> GetQuestionBySurveyId(int surveyId) {
            if (_questions == null) {
                await GetQuestionsFromDbCache();
            }

            var questionBySurveyId = _questions.FindAll(x => x.SurveyId == surveyId);
            if (questionBySurveyId == null) {
                throw new ArgumentNullException(nameof(questionBySurveyId));
            }

            return questionBySurveyId;
        }


        private bool _gettingQuestionsFromDbCache = false;


        internal async Task<QuestionModel> GetQuestionById(int questionId) {
            if (_questions == null) {
                await GetSurveysFromDbCache();
            }

            var question = _questions.First(q => q.Id == questionId);
            if (question == null) {
                throw new ArgumentNullException(nameof(question));
            }

            return question;
        }

        internal async Task<QuestionDTO> GetQuestionDTOById(int questionId) => await _httpClient.GetFromJsonAsync<QuestionDTO>($"{ApiEndpoints.s_questionsDTO}/{questionId}");




        internal async Task GetQuestionsFromDbCache() {
            if (_gettingQuestionsFromDbCache == false) {
                _gettingQuestionsFromDbCache = true;
                _questions = await _httpClient.GetFromJsonAsync<List<QuestionModel>>(ApiEndpoints.s_questions);
                _gettingQuestionsFromDbCache = false;
            }

        }

        internal event Action OnQuestionsDataChanged;

        private void NotifyQuestionsDataChanges() => OnQuestionOptionsDataChanged?.Invoke();
        #endregion

        #region QuestionOptions
        private List<QuestionOptionModel> _questionOptions = null;
        internal List<QuestionOptionModel> QuestionOptions {
            get { return _questionOptions; }
            set {
                _questionOptions = value;
                NotifyQuestionOptionsDataChanges();
            }
        }


        internal async Task<List<QuestionOptionModel>> GetQuestionOptionsByQuestionId(int questionId) {
            if (_questionOptions == null) {
                await GetQuestionOptionsFromDbCache();
            }

            var questionOptionsByQuestionId = _questionOptions.FindAll(x => x.QuestionId == questionId);
            if (questionOptionsByQuestionId == null) {
                throw new ArgumentNullException(nameof(questionOptionsByQuestionId));
            }

            return questionOptionsByQuestionId;
        }


        private bool _gettingQuestionOptionsFromDbCache = false;


        internal async Task<QuestionOptionModel> GetQuestionOptionById(int questionOptionId) {
            if (_questions == null) {
                await GetSurveysFromDbCache();
            }

            var questionOptions = _questionOptions.First(q => q.Id == questionOptionId);
            if (questionOptions == null) {
                throw new ArgumentNullException(nameof(questionOptions));
            }

            return questionOptions;
        }

        internal async Task<QuestionOptionDTO> GetQuestionOptionsDTOById(int questionOptionId) => await _httpClient.GetFromJsonAsync<QuestionOptionDTO>($"{ApiEndpoints.s_questionOptionsDTO}/{questionOptionId}");




        internal async Task GetQuestionOptionsFromDbCache() {
            if (_gettingQuestionOptionsFromDbCache == false) {
                _gettingQuestionOptionsFromDbCache = true;
                _questionOptions = await _httpClient.GetFromJsonAsync<List<QuestionOptionModel>>(ApiEndpoints.s_questionOptions);
                _gettingQuestionOptionsFromDbCache = false;
            }

        }

        internal event Action OnQuestionOptionsDataChanged;

        private void NotifyQuestionOptionsDataChanges() => OnQuestionOptionsDataChanged?.Invoke();
        #endregion
    }
}
