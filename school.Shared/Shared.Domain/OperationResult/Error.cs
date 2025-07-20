namespace Shared.Domain.OperationResult;

public class Error: IEquatable<Error>
{
    
    public static readonly Error NullValue = new Error("Error.NullValue","The Speified result value is null");

    public static readonly Error InvalidUserType = new Error("Error.InvalidUserType","The specified user type is invalid");

    public static readonly Error InvalidApiKey = new Error("Error.InvalidApiKey","The specified api key is invalid");
    public static readonly Error MissingApiKey=  new Error("Error.MissingApiKey","The specified API key is missing");
    
    public static readonly Error InvalidCredential=  new Error("Error.InvalidCredential","Your credentials are invalid");
    
    public static readonly Error UnAuthorized=  new Error("Error.UnAuthorized","unauthorized");

    public static readonly Error AlreadyProcessed=  new Error("Error.AlreadyProcessed","this Request is Already Processed");
    
    public static  Error ValidationFailures(string error) =>new Error("Error.ValidationFailures",error);
    public static  Error NotFound(string message) => new Error("Error.NotFound",message);
    
    public static  Error Internal(string message) => new Error("Error.Internal",message);
    
  
    
    
    
    
    
    
    public Error(string code, string message)
    {
        
        Message=message;
        Code=code;
        
    }
    
    public string Message { get; set; }
    
    public string Code { get; set; }
    
    public static implicit operator string(Error error)=>error.Code;
    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return this.Code == other.Code && this.Message == other.Message;
    }

    
}