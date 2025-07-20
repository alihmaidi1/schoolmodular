namespace Shared.Infrastructure.Services.Archive;

public class ArchiveRecord
{
    public Guid Id { get; set; }

    public ArchiveRecord()
    {
        
        Id=Guid.NewGuid();
    }
    
    public Guid EntityId { get; set; }    
    public string EntityName { get; set; }
    public string JsonData { get; set; }

    public DateTime ArchivedAt { get; set; } = DateTime.UtcNow;


}