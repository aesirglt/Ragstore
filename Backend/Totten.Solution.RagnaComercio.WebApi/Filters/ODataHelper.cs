namespace Totten.Solution.RagnaComercio.WebApi.Filters;

using System;

/// <summary>
/// 
/// </summary>
public static class ODataHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static List<string> ExtractItemIdsFromFilter(string filter)
    {
        var results = new List<string>();

        if (string.IsNullOrWhiteSpace(filter))
            return results;

        ReadOnlySpan<char> span = filter;

        while (!span.IsEmpty)
        {
            int eqIndex = span.IndexOf("itemId eq ", StringComparison.OrdinalIgnoreCase);
            if (eqIndex >= 0)
            {
                span = span[( eqIndex + "itemId eq ".Length )..];

                int end = span.IndexOfAny(" &|)");
                ReadOnlySpan<char> valueSpan = end >= 0 ? span[..end] : span;

                string value = valueSpan.Trim().Trim('\'').ToString();
                if (!string.IsNullOrEmpty(value))
                    results.Add(value);

                span = end >= 0 ? span[end..] : ReadOnlySpan<char>.Empty;
                continue;
            }

            int inIndex = span.IndexOf("itemId in (", StringComparison.OrdinalIgnoreCase);
            if (inIndex >= 0)
            {
                span = span[$"{inIndex}itemId in (".Length..];
                int closeParen = span.IndexOf(')');

                if (closeParen >= 0)
                {
                    var listSpan = span[..closeParen];
                    foreach (var rawValue in listSpan.ToString().Split(','))
                    {
                        var value = rawValue.Trim().Trim('\'');
                        if (!string.IsNullOrEmpty(value))
                            results.Add(value);
                    }

                    span = span[( closeParen + 1 )..];
                    continue;
                }
            }

            break;
        }

        return results;
    }
}