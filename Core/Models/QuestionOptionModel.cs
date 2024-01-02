using Core.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionOptionModel {
        public int Id { get; set; }

        [NoThreeMoreSpacesInARow(ErrorMessage = "The category description contains three or more spaces. Please, remove them")]
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }
}
