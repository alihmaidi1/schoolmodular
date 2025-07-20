using Microsoft.AspNetCore.Authorization;

namespace Shared.Infrastructure.Authorization;

public class PermissionRequirement: IAuthorizationRequirement
{
    public string Permission { get; set; }    


    public PermissionRequirement(string Permission)
    {
        this.Permission = Permission;
        

    }
    
    
}