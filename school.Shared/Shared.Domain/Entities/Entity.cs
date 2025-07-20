namespace Shared.Domain.Entities;

public class Entity: IEntity
{
    public Guid Id { get; protected set; }

    
    public string CreatedBy { get; private set; } = "";
    public DateTime CreatedAt { get; private set; }=DateTime.UtcNow;
    public string LastModifiedBy { get; private set; } = "";
    public DateTime LastModified { get; private set; }=DateTime.UtcNow;
    public void SetCreatedBy(string newBy)
    {
        CreatedBy = newBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetModified(string newBy)
    {
        LastModifiedBy = newBy;
        LastModified = DateTime.UtcNow;
    }
}