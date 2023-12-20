using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionOptionModel {
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
    }
}
