namespace Core.Models {
    public class SurveyModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<SectionModel> Sections { get; set; }
    }
}
