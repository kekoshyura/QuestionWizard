using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionOptionDTO {
        public int Id { get; set; }
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}
