namespace Totten.Solution.RagnaComercio.WebApi.Filters;

using System.Text.RegularExpressions;

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
            int eqIndex = span.IndexOf("itemId eq '", StringComparison.OrdinalIgnoreCase);
            if (eqIndex >= 0)
            {
                span = span[($"{eqIndex}itemId eq '".Length )..];
                int endQuote = span.IndexOf('\'');

                if (endQuote >= 0)
                {
                    var value = span[..endQuote].ToString();
                    results.Add(value);
                    span = span[( endQuote + 1 )..];
                    continue;
                }
            }

            int inIndex = span.IndexOf("itemId in (", StringComparison.OrdinalIgnoreCase);
            if (inIndex >= 0)
            {
                span = span[( $"{inIndex}itemId in (".Length )..];
                int closeParen = span.IndexOf(')');

                if (closeParen >= 0)
                {
                    var listSpan = span[..closeParen];
                    while (!listSpan.IsEmpty)
                    {
                        int start = listSpan.IndexOf('\'');
                        if (start < 0) break;

                        listSpan = listSpan.Slice(start + 1);
                        int end = listSpan.IndexOf('\'');
                        if (end < 0) break;

                        var value = listSpan.Slice(0, end).ToString();
                        results.Add(value);
                        listSpan = listSpan.Slice(end + 1);

                        int comma = listSpan.IndexOf(',');
                        if (comma >= 0)
                        {
                            listSpan = listSpan.Slice(comma + 1);
                        }
                        else
                        {
                            break;
                        }
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