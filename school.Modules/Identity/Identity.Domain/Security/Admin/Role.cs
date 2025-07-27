using Shared.Domain.Entities;

namespace Identity.Domain.Security.Admin;

public class Role: Entity, IEntity
{

    private Role()
    {
        
        Id= Guid.NewGuid();
    }

    public static Role Create(string roleName,List<string> permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);
        return new Role()
        {
            Name = roleName,
            Permissions = permissions.Select(x=>x.ToString()).ToList()

        };

    }
    public string Name { get; private set; }
    
    public List<string> Permissions { get; private set; }
    
    
    public ICollection<Admin>  Admins { get; private set; }

    
}