
using System.ComponentModel.DataAnnotations;

namespace Core.Models.CustomValidation {
    public class NoPeriods : ValidationAttribute {
        public override bool IsValid(object value) {
            string input = value.ToString();
            bool noPeriods = input.Contains('_') == false;
            return noPeriods;
        }
    }
}
