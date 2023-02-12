// using System.Text.Encodings.Web;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Options;
//
// namespace API.Handler;
//
// public class BoatAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
// {
//     public BoatAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, 
//         ILoggerFactory logger, 
//         UrlEncoder encoder, 
//         ISystemClock clock): base(option, logger, encoder, clock)
//     {
//     }
//     protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
//     {
//         return AuthenticateResult.Fail("Not Implemented");
//     }
// }