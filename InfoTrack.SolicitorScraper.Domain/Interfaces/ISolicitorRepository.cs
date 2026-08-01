using InfoTrack.SolicitorScraper.Domain.Entities;

namespace InfoTrack.SolicitorScraper.Domain.Interfaces;

public interface ISolicitorRepository
{
    Task<List<SolicitorDirectoryEntry>> GetAllAsync();
    Task<List<SolicitorDirectoryEntry>> GetByLocationAsync(Guid locationId);
    Task ReplaceAllAsync(IEnumerable<SolicitorDirectoryEntry> solicitors);
}