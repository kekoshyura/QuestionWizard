using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class SurveyModel {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        public ICollection<SectionModel> Sections { get; set; }
    }
}
