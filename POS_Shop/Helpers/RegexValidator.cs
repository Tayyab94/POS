using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    public static class RegexValidator
    {
        // Common regex patterns
        public const string LettersAndSpaces = "^[a-zA-Z ]*$";
        public const string NumbersOnly = "^[0-9]*$";
        public const string NumbersWithDecimal = "^[0-9]*\\.?[0-9]*$";
        public const string Alphanumeric = "^[a-zA-Z0-9 ]*$";
        public const string Email = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";

        private static readonly Dictionary<string, string> _lastValidTexts = new Dictionary<string, string>();

        /// <summary>
        /// Validates text against a regex pattern and returns valid text
        /// </summary>
        /// <param name="inputText">The text to validate</param>
        /// <param name="regexPattern">Regex pattern to validate against</param>
        /// <param name="identifier">Unique identifier for storing last valid text</param>
        /// <param name="storeLastValid">Whether to store the last valid text</param>
        /// <returns>Valid text (either original if valid, or last valid text if invalid)</returns>
        public static string ValidateAndRevert(string inputText, string regexPattern, string identifier = null, bool storeLastValid = true)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                return string.Empty;
            }

            if (Regex.IsMatch(inputText, regexPattern))
            {
                if (storeLastValid && !string.IsNullOrEmpty(identifier))
                {
                    // Store the valid text
                    if (_lastValidTexts.ContainsKey(identifier))
                        _lastValidTexts[identifier] = inputText;
                    else
                        _lastValidTexts.Add(identifier, inputText);
                }
                return inputText;
            }
            else
            {
                // Revert to last valid text if available and identifier provided
                if (!string.IsNullOrEmpty(identifier) && _lastValidTexts.ContainsKey(identifier) && !string.IsNullOrEmpty(_lastValidTexts[identifier]))
                {
                    return _lastValidTexts[identifier];
                }
                else
                {
                    // If no valid text stored, return empty string
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Validates text against a common predefined pattern
        /// </summary>
        public static string ValidateCommonPattern(string inputText, ValidationPattern pattern, string identifier = null, bool storeLastValid = true)
        {
            string regexPattern = GetRegexPattern(pattern);
            return ValidateAndRevert(inputText, regexPattern, identifier, storeLastValid);
        }

        /// <summary>
        /// Gets the regex pattern based on ValidationPattern enum
        /// </summary>
        private static string GetRegexPattern(ValidationPattern pattern)
        {
            switch (pattern)
            {
                case ValidationPattern.LettersAndSpaces:
                    return LettersAndSpaces;
                case ValidationPattern.NumbersOnly:
                    return NumbersOnly;
                case ValidationPattern.NumbersWithDecimal:
                    return NumbersWithDecimal;
                case ValidationPattern.Alphanumeric:
                    return Alphanumeric;
                case ValidationPattern.Email:
                    return Email;
                default:
                    return LettersAndSpaces;
            }
        }

        /// <summary>
        /// Simple validation without storing last valid text
        /// </summary>
        public static bool IsValid(string inputText, string regexPattern)
        {
            if (string.IsNullOrEmpty(inputText))
                return true; // Empty is considered valid

            return Regex.IsMatch(inputText, regexPattern);
        }

        /// <summary>
        /// Simple validation using common pattern without storing last valid text
        /// </summary>
        public static bool IsValidCommonPattern(string inputText, ValidationPattern pattern)
        {
            string regexPattern = GetRegexPattern(pattern);
            return IsValid(inputText, regexPattern);
        }

        /// <summary>
        /// Clears the stored last valid text for a specific identifier
        /// </summary>
        public static void ClearStoredText(string identifier)
        {
            if (!string.IsNullOrEmpty(identifier) && _lastValidTexts.ContainsKey(identifier))
            {
                _lastValidTexts.Remove(identifier);
            }
        }

        /// <summary>
        /// Clears all stored last valid texts
        /// </summary>
        public static void ClearAllStoredTexts()
        {
            _lastValidTexts.Clear();
        }

        /// <summary>
        /// Gets the last valid text for an identifier
        /// </summary>
        public static string GetLastValidText(string identifier)
        {
            if (!string.IsNullOrEmpty(identifier) && _lastValidTexts.ContainsKey(identifier))
            {
                return _lastValidTexts[identifier];
            }
            return string.Empty;
        }
    }
}
