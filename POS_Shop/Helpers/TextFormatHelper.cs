using System.Text;
using System.Text.RegularExpressions;

namespace POS_Shop.Helpers
{
    public static class TextFormatHelper
    {
        private static readonly Regex _englishPattern = new Regex(@"[0-9A-Za-z\-\.]+", RegexOptions.Compiled);
        private const char LRM = '\u200E'; // Left-to-Right Mark
        private const char RLM = '\u200F'; // Right-to-Left Mark

        public static string FormatMixedText(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // For very short strings, avoid StringBuilder overhead
            if (input.Length < 3)
                return RLM + input;

            var result = new StringBuilder(input.Length + 10).Append(RLM);
            int lastIndex = 0;

            foreach (Match match in _englishPattern.Matches(input))
            {
                // Add Urdu text before this English segment
                if (match.Index > lastIndex)
                {
                    result.Append(input, lastIndex, match.Index - lastIndex);
                }

                // Wrap English segment with LRM to keep it LTR
                result.Append(LRM);
                result.Append(match.Value);
                result.Append(RLM);

                lastIndex = match.Index + match.Length;
            }

            // Add remaining Urdu text
            if (lastIndex < input.Length)
            {
                result.Append(input, lastIndex, input.Length - lastIndex);
            }

            return result.ToString();
        }

        // You can add other related text formatting methods here in the future
        public static string FormatUrduText(string input)
        {
            // Example of another related method you might add
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return RLM + input.Trim() + LRM;
        }

        public static string TruncateWithEllipsis(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;

            return input.Length > maxLength ? input.Substring(0, maxLength) + "..." : input;
        }


        public static string RemoveDirectionalCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove RTL, LTR, and other directional control characters
            return input.Replace("\u200E", "")  // Left-to-Right Mark
                       .Replace("\u200F", "")  // Right-to-Left Mark
                       .Replace("\u200B", "")  // Zero Width Space
                       .Replace("\u200C", "")  // Zero Width Non-Joiner
                       .Replace("\u200D", "")  // Zero Width Joiner
                       .Replace("\uFEFF", ""); // Zero Width No-Break Space
        }
    }

    

        //private string FormatMixedText(string input)
        //{
        //    if (string.IsNullOrWhiteSpace(input))
        //        return input;

        //    // Use these directional marks
        //    const char LRM = '\u200E'; // Left-to-Right Mark
        //    const char RLM = '\u200F'; // Right-to-Left Mark

        //    // Pattern to find English/number sequences (keep them together)
        //    var englishPattern = new System.Text.RegularExpressions.Regex(@"[0-9A-Za-z\-\.]+");

        //    // Start with RLM for overall RTL context
        //    var result = new System.Text.StringBuilder().Append(RLM);

        //    int lastIndex = 0;

        //    foreach (System.Text.RegularExpressions.Match match in englishPattern.Matches(input))
        //    {
        //        // Add Urdu text before this English segment
        //        if (match.Index > lastIndex)
        //        {
        //            result.Append(input.Substring(lastIndex, match.Index - lastIndex));
        //        }

        //        // Wrap English segment with LRM to keep it LTR
        //        result.Append(LRM);
        //        result.Append(match.Value);
        //        result.Append(RLM);

        //        lastIndex = match.Index + match.Length;
        //    }

        //    // Add remaining Urdu text
        //    if (lastIndex < input.Length)
        //    {
        //        result.Append(input.Substring(lastIndex));
        //    }

        //    return result.ToString();
        //}
    }
