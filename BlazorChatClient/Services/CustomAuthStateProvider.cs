//using BlazorChatClient.Services;
//using Microsoft.AspNetCore.Components.Authorization;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace BlazorChatClient.Services
//{
//    public class CustomAuthStateProvider : AuthenticationStateProvider
//    {
//        private readonly AuthService _authService;

//        public CustomAuthStateProvider(AuthService authService)
//        {
//            _authService = authService;
//        }

//        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            var token = await _authService.GetAuthTokenAsync();
//            if (string.IsNullOrEmpty(token))
//            {
//                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//            }

//            // Create a ClaimsPrincipal with the token if needed
//            var claims = new[] { new Claim(ClaimTypes.Name, "User") }; // Replace with actual claims
//            var identity = new ClaimsIdentity(claims, "jwt");
//            var user = new ClaimsPrincipal(identity);

//            return new AuthenticationState(user);
//        }

//        public async Task NotifyUserAuthentication(string token)
//        {
//            var claims = new[] { new Claim(ClaimTypes.Name, "User") }; // Replace with actual claims
//            var identity = new ClaimsIdentity(claims, "jwt");
//            var user = new ClaimsPrincipal(identity);

//            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//        }

//        public void NotifyUserLogout()
//        {
//            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
//        }
//    }
//}
