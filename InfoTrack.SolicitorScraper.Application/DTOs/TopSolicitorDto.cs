namespace InfoTrack.SolicitorScraper.Application.DTOs;

public class TopSolicitorDto
{
    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }
}