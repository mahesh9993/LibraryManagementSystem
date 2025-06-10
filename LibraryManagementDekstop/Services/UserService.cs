using LibraryManagementDekstop.Interfaces;
using LibraryManagementDekstop.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LibraryManagementDekstop.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<bool> SaveUserDetails(UserSaveModel userSaveModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/User/SaveUserDetails", userSaveModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> GetUserDetails(UserSaveModel userSaveModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/User/GetUserDetails", userSaveModel);
            return response.IsSuccessStatusCode;
        }
    }
}
