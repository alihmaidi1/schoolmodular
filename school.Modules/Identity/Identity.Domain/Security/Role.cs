using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Security;

public class Role: IdentityRole<Guid>
{

    public Role()
    {
        
        Id= Guid.NewGuid();
    }
    
}