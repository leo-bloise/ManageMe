using System.Text;
using System.Text.RegularExpressions;

namespace ManageMe.Api.Services;

public static class StringExtensions
{
    public static string ToCamelCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var words = Regex.Split(text, @"[\s_\-]+");

        var sb = new StringBuilder();

        for(int i = 0; i < words.Length; i++)
        {
            var word = words[i].ToLower();

            if(i == 0)
            {
                sb.Append(word);
                continue;
            }

            sb.Append(char.ToUpper(word[0]) + word.Substring(1));
        }

        return sb.ToString();
    }
}
