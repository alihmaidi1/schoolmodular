using Shared.Domain.Entities;
using Shared.Domain.OperationResult;

namespace Identity.Domain.Security;

public class RefreshToken: Entity,IEntity
{

    private RefreshToken()
    {
        
        Id=Guid.NewGuid();
    }

    public static TResult<RefreshToken> Create(string refreshToken)
    {
        if (refreshToken == null)
        {
            return Result.InternalError<RefreshToken>(Error.Internal("RefreshToken is null"));
        }
        return Result.Success(new RefreshToken()
        {
            Value = refreshToken
        });
    }
    public string Value { get; set; }
    
    
    
}