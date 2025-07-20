// Licensed to the .NET Foundation under one or more agreements.

namespace Shared.Infrastructure.Services.Whatsapp;

public interface IWhatsAppService
{
    Task<WhatsAppApiResponse> SendWhatsAppMessage(string to, string message);

}
