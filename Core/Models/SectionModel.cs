using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class SectionModel {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}
