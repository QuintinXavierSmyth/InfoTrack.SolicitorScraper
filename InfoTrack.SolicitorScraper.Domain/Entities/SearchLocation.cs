namespace InfoTrack.SolicitorScraper.Domain.Entities;

public class SearchLocation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UrlSlug { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public DateTime? LastScraped { get; set; }
    public List<SolicitorDirectoryEntry> DirectoryEntries { get; set; } = new();
}