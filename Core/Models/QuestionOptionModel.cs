using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class QuestionOptionModel {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Text { get; set; }
    }
}
