using System.Net;
using Identity.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Domain.OperationResult;

namespace Identity.infrastructure.Authorization;

public class UsertypeAuthorize: AuthorizeAttribute, IAuthorizationFilter
{
    
    private UserType _userType;
    public UsertypeAuthorize(UserType userType)
    {
        _userType=userType;
        
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
        var userType=context.HttpContext.User.Claims.FirstOrDefault(c => c.Type=="UserType")?.Value;
        if (userType!=_userType.ToString())
        {
            var result = new TResult<object>(null,false,HttpStatusCode.Unauthorized,Error.UnAuthorized)
            {


            };
            context.Result = new JsonResult(result)
            {
                
                    StatusCode = (int)HttpStatusCode.Unauthorized
            };

        }
    }
}