namespace Shared.Domain.Entities;

public interface IEntity
{
    Guid Id { get;}

    string CreatedBy { get; }
    
    DateTime CreatedAt { get; }
    
    
    string LastModifiedBy { get; }
    
    DateTime LastModified { get; }
    
    void SetCreatedBy(string newBy);
    void SetModified(string newBy);
    
}