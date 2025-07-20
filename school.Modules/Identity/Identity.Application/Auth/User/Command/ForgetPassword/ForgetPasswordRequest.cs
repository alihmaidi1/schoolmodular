// using Microsoft.AspNetCore.Http;
// using Shared.Domain.CQRS;
//
// namespace Identity.Application.Auth.User.Command.ForgetPassword;
//
// public class ForgetPasswordRequest
// {
//     
//     public string Email { get; set; }
//     
// }
//
//
// public sealed class ForgetPasswordCommand : ForgetPasswordRequest,ICommand<IResult>
// {
//     public Guid? RequestId { get; set; }
// }