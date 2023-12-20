namespace Core.Models {
    public class SectionModel {
        public int Id { get; set; }

        public string SectionImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<SurveyModel> Surveys { get; set; }
    }
}
