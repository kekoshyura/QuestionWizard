using static Core.Common.Common;

namespace Core.Models {
    public class QuestionModel {
        public int Id { get; set; }
        public string Text { get; set; }
        public ControlType ControlType { get; set; }
        public ICollection<QuestionOptionModel> Options { get; set; }
    }
}
