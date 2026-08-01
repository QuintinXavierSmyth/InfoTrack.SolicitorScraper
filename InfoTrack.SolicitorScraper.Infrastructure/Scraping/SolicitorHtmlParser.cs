using System.Text.RegularExpressions;
using InfoTrack.SolicitorScraper.Domain.Entities;

namespace InfoTrack.SolicitorScraper.Infrastructure.Scraping;

public class SolicitorHtmlParser
{
    public List<SolicitorDirectoryEntry> Parse(
        string html,
        SearchLocation location)
    {
        var results = new List<SolicitorDirectoryEntry>();

        var matches = Regex.Matches(
            html,
            @"<div class=""result-item"">(.*?)(?=<div class=""result-item""|$)",
            RegexOptions.Singleline);

        foreach (Match match in matches)
        {
            var solicitor = ParseSolicitor(
                match.Value,
                location);

            if (solicitor != null)
            {
                results.Add(solicitor);
            }
        }
        return results;
    }

    private SolicitorDirectoryEntry? ParseSolicitor(
        string html,
        SearchLocation location)
    {
        var name = Extract(
            html,
            @"<span class=""h2"">(.*?)<div");

        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return new SolicitorDirectoryEntry
        {
            Id = Guid.NewGuid(),

            SearchLocationId = location.Id,

            Name = Clean(name),

            IsVerified = html.Contains("greentick"),

            Rating = ExtractRating(html),

            ReviewCount = ExtractReviews(html),

            PhoneNumber = Extract(
                html,
                @"tel:[^""]*"">(.*?)</a>"),

            Address = Extract(
                html,
                @"<address[^>]*>(.*?)</address>"),

            Description = Extract(
                html,
                @"<p[^>]*>(.*?)</p>"),

            EmailUrl = Extract(
                html,
                @"href=""([^""]*enquiry-form[^""]*)"""),

            WebsiteUrl = Extract(
                html,
                @"<a[^>]*href=""(https?://[^""]+)""[^>]*>.*?Website"),

            ViewMoreUrl = BuildViewMoreUrl(html),

            ScrapedAt = DateTime.UtcNow
        };
    }

    private string BuildViewMoreUrl(string html)
    {
        var relativeUrl = Extract(
            html,
            @"<a href=""([^""]+)"" class=""link-map""");

        if (string.IsNullOrWhiteSpace(relativeUrl))
        {
            return string.Empty;
        }

        if (relativeUrl.StartsWith("http"))
        {
            return relativeUrl;
        }

        return $"https://www.solicitors.com{relativeUrl}";
    }

    private string Extract(
        string html,
        string pattern)
    {
        var match = Regex.Match(
            html,
            pattern,
            RegexOptions.Singleline | RegexOptions.IgnoreCase);

        return match.Success
            ? Clean(match.Groups[1].Value)
            : string.Empty;
    }

    private decimal ExtractRating(string html)
    {
        var ratingSection = Regex.Match(
            html,
            @"<span class=""rev-results"">(.*?)</span>",
            RegexOptions.Singleline | RegexOptions.IgnoreCase);


        if (!ratingSection.Success)
        {
            return 0;
        }


        var ratingHtml = ratingSection.Groups[1].Value;


        var fullStars = Regex.Matches(
            ratingHtml,
            "star-full",
            RegexOptions.IgnoreCase)
            .Count;


        var halfStar = Regex.IsMatch(
            ratingHtml,
            "star-half",
            RegexOptions.IgnoreCase)
            ? 0.5m
            : 0m;


        return fullStars + halfStar;
    }

    private int ExtractReviews(string html)
    {
        var match = Regex.Match(
            html,
            @"\((\d+)\)");

        return match.Success
            ? int.Parse(match.Groups[1].Value)
            : 0;
    }

    private string Clean(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        value = Regex.Replace(
            value,
            "<.*?>",
            string.Empty);

        value = System.Net.WebUtility.HtmlDecode(value);

        value = value.Replace(
            "|",
            ", ");

        value = Regex.Replace(
            value,
            @"\s+",
            " ");

        value = Regex.Replace(
            value,
            @",\s*,",
            ",");

        return value.Trim(' ', ',');
    }
}