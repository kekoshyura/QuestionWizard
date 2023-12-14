using System.ComponentModel.DataAnnotations;
using static Core.Common.Common;

namespace Core.Models {
    public class QuestionModel {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Text { get; set; }

        [EnumDataType(typeof(ControlType))]
        public ControlType ControlType { get; set; }

        public ICollection<QuestionOptionModel> Options { get; set; }
    }
}
