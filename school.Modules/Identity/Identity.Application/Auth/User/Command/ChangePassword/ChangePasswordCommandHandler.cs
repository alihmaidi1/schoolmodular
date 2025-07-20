// using Microsoft.AspNetCore.Http;
// using Shared.Application.Services.User;
// using Shared.Domain.CQRS;
//
// namespace Identity.Application.Auth.User.Command.ChangePassword;
//
// internal sealed class ChangePasswordCommandHandler: ICommandHandler<ChangePasswordCommand>
// {
//     private readonly ICurrentUserService _currentUserService;
//     public ChangePasswordCommandHandler(ICurrentUserService  currentUserService)
//     {
//         
//         _currentUserService=currentUserService;
//     }
//     
//     public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
//     {
//
//         Console.WriteLine(_currentUserService.UserId);
//         return Results.Ok("sdfs");
//         // return ("password changed");
//     }
// }