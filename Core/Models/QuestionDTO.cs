using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionDTO {
        public int Id { get; set; }
        public string Text { get; set; }
        public ControlType ControlType { get; set; }
        [Required]
        public int SurveyId { get; set; }
    }
}
