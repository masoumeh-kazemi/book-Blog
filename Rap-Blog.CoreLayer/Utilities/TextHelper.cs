﻿using System.Text.RegularExpressions;

namespace Rap_Blog.CoreLayer.Utilities;

public static class TextHelper
{
    public static string ToSlug(this string value)
    {
        return value.Trim().ToLower()
            .Replace("@", "")
            .Replace("#", "")
            .Replace("$", "")
            .Replace("%", "")
            .Replace("^", "")
            .Replace("*", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("+", "")
            .Replace("~", "")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("/", "")
            .Replace(@"\", "")
            .Replace(" ", "_");


    }

    public static string ConvertHtmlToText(this string text)
    {
        return Regex.Replace(text, "<.*?>", " ").Replace(":&nbsp;", " ");
    }

}