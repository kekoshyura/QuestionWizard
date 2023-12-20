using System.ComponentModel.DataAnnotations;

namespace Core.Models {
    public class SurveyModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public int SectionId { get; set; }
        public SectionModel Section { get; set; }

        public List<QuestionModel> Questions { get; set; }

    }
}
