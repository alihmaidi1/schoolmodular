// Licensed to the .NET Foundation under one or more agreements.

namespace Shared.Infrastructure.Services.Whatsapp;

public class WhatsAppMessageRequest
{
    public string messaging_product { get; set; } = "whatsapp";

    public string recipient_type { get; set; } = "individual";
    
    public string to { get; set; }


    public string type { get; set; } = "text";
    
    
    public TextContent text { get; set; }
}
public class TextContent
{
    public string body { get; set; }
}