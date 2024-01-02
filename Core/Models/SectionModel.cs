using Core.Models.CustomValidation;

namespace Core.Models {
    public class SectionModel {
        public int Id { get; set; }

        public string SectionImagePath { get; set; }

        [NoPeriods(ErrorMessage = "The category name contains one ore more periods. Please, remove them")]
        public string Title { get; set; }

        [NoThreeMoreSpacesInARow(ErrorMessage = "The category description contains three or more spaces. Please, remove them")]
        public string Description { get; set; }
        public List<SurveyModel> Surveys { get; set; }
    }
}
