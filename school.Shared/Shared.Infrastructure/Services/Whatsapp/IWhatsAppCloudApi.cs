// Licensed to the .NET Foundation under one or more agreements.

using Refit;

namespace Shared.Infrastructure.Services.Whatsapp;

public interface IWhatsAppCloudApi
{
    
    [Post("/{phoneNumberId}/messages")]
    Task<WhatsAppApiResponse> SendMessage(
        [AliasAs("phoneNumberId")] string phoneNumberId,
        [Body] WhatsAppMessageRequest request,
        [Header("Authorization")] string authorization);
    
}
