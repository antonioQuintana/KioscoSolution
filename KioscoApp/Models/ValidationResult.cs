using System.Collections.Generic;

namespace KioscoApp.Models
{
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public Dictionary<string, string> Errors { get; private set; }

        public ValidationResult()
        {
            Errors = new Dictionary<string, string>();
        }

        public void AddError(string propertyName, string errorMessage)
        {
            if (!Errors.ContainsKey(propertyName))
            {
                Errors.Add(propertyName, errorMessage);
            }
        }
    }
}
