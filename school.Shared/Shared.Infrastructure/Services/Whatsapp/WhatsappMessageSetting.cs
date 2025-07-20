// Licensed to the .NET Foundation under one or more agreements.

using System.ComponentModel.DataAnnotations;

namespace Shared.Infrastructure.Services.Whatsapp;

public class WhatsappMessageSetting
{
    
    
    [Required]
    public string AccessToken { get; set; }
    
    [Required]
    public string PhoneNumberId { get; set; }
    
    [Required]
    
    public string BaseUrl { get; set; }
}
