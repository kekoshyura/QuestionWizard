using Core.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class SurveyModel {
        public int Id { get; set; }

        [NoPeriods(ErrorMessage = "The category name contains one ore more periods. Please, remove them")]
        public string Title { get; set; }

        [NoThreeMoreSpacesInARow(ErrorMessage = "The category description contains three or more spaces. Please, remove them")]
        public string Description { get; set; }

        [Required]
        public int SectionId { get; set; }
        public SectionModel Section { get; set; }

        public List<QuestionModel> Questions { get; set; }

    }
}
