using System.ComponentModel.DataAnnotations;

namespace Core.Models.CustomValidation {
    public class NoThreeMoreSpacesInARow : ValidationAttribute {
        public override bool IsValid(object value) {
            string input = value.ToString();
            bool noThreeSpaces = true;

            for (int i = 2; i < input.Length; i++) {
                if (input[i] == ' ' && input[i] == input[i - 1] && input[i] == input[i - 2]) {
                    noThreeSpaces = false;
                }
            }
            return noThreeSpaces;
        }
    }
}
