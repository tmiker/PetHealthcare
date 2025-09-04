using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace PetHealthcare.MVC.HttpProviders
{
    public class AuthenticationHttpProvider : IAuthenticationHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> RegisterUserAsync(RegisterUserDTO registerDTO)
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AuthPath}/register";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(registerDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else
            {
                var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                return (false, errorMessages);
            }
        }

        public async Task<(bool IsSuccess, LoginResponseDTO? ResponseDTO, List<string>? ErrorMessages)> LoginUserAsync(LoginUserDTO loginDTO)
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AuthPath}/login";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return (true, result, null);
            }
            else
            {
                var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                if (errorMessages != null && errorMessages.Count > 0) return (false, null, errorMessages);
                else return (false, null, new List<string> { "Unknown error logging in." });
            }
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AuthPath}/updatePassword";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(updatePasswordDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return (true, null);
            }
            else
            {
                var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                return (false, errorMessages);
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, List<string>? ErrorMessages)> DeleteAccountAsync(DeleteAccountDTO deleteAccountDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AuthPath}/deleteAccount";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(deleteAccountDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var successMessage = await response.Content.ReadAsStringAsync();
                if (successMessage != null && successMessage.Length > 0) return (true, successMessage, null);
                else return (true, null, null);
            }
            else
            {
                var errorMessages = await response.Content.ReadFromJsonAsync<List<string>>();
                return (false, null, errorMessages);
            }
        }
    }
}
