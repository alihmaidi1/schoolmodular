namespace Shared.Application.CQRS;

public class PiplineOrderAttribute:Attribute
{
    public int Order { get; set; }
    public PiplineOrderAttribute(int order)
    {
        ArgumentNullException.ThrowIfNull(order);
        Order = order;
        
    }
    
}