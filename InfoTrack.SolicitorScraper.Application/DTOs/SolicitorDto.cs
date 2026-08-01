namespace InfoTrack.SolicitorScraper.Application.DTOs;

public class SolicitorDto
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public bool IsVerified { get; set; }

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string WebsiteUrl { get; set; } = string.Empty;

    public string ViewMoreUrl { get; set; } = string.Empty;
}