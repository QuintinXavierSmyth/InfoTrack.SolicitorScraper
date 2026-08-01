namespace InfoTrack.SolicitorScraper.Domain.Entities;

public class SolicitorDirectoryEntry
{
    public Guid Id { get; set; }
    public Guid SearchLocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string EmailUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string ViewMoreUrl { get; set; } = string.Empty;
    public DateTime ScrapedAt { get; set; }
}