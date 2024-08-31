using BlazorChatClient.Model;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace BlazorChatClient.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> RegisterAsync(UserModel userModel)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7051/api/Account/register", userModel);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> LoginAsync(UserModel userModel)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7051/api/Account/login", userModel);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("authToken", token);
                return "Login successful.";
            }
            return "Login failed.";
        }

        public async Task<string> LogoutAsync()
        {
            var response = await _httpClient.PostAsync("https://localhost:7051/api/Account/logout", null);
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("authToken");
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> GetAuthTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
        public async Task<List<string>> GetRegisteredUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7051/api/users/all");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<string>>();
                return users ?? new List<string>();
            }
            return new List<string>();
        }

    }
}
