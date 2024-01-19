namespace Core.Models {
    public class SurveyDTO {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SurveyImagePath { get; set; }

        public int SectionId { get; set; }

        //public List<QuestionModel> Questions { get; set; }

    }
}
