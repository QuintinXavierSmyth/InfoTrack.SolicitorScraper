namespace InfoTrack.SolicitorScraper.Application.DTOs;

public class LocationSummaryDto
{
    public string Location { get; set; } = string.Empty;

    public int SolicitorCount { get; set; }

    public bool IsEnabled { get; set; }
}