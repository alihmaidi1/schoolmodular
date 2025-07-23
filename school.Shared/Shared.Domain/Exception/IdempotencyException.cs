namespace Shared.Domain.Exception;

public class IdempotencyException: System.Exception
{

    public IdempotencyException(string message): base(message)
    {
        
    }
    
}