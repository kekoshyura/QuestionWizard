using Core.Common;
using Core.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionModel {
        public int Id { get; set; }

        [NoThreeMoreSpacesInARow(ErrorMessage = "The category description contains three or more spaces. Please, remove them")]
        public string Text { get; set; }
        public ControlType ControlType { get; set; }
        [Required]
        public int SurveyId { get; set; }
        public SurveyModel Survey { get; set; }

        public List<QuestionOptionModel> QuestionOptions { get; set; }
    }
}
