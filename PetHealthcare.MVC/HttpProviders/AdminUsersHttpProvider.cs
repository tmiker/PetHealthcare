using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;

namespace PetHealthcare.MVC.HttpProviders
{
    public class AdminUsersHttpProvider : IAdminUsersHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUsersHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<UserInfoDTO>? UserInfoDTOs, string? ErrorMessage)> GetAllUsersAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/getAllUsers";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<UserInfoDTO>? users = await response.Content.ReadFromJsonAsync<IEnumerable<UserInfoDTO>>();
                return (true, users, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to retrieve the users.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> LockAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/lockUser?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UnLockAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/unlockUser?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserAdminRoleAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/flipUserAdminRole?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string successMessage = await response.Content.ReadAsStringAsync();
                if (successMessage != null && successMessage.Length > 0) return (true, successMessage, null);
                else return (true, null, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserManagerRoleAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/flipUserManagerRole?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string successMessage = await response.Content.ReadAsStringAsync();
                if (successMessage != null && successMessage.Length > 0) return (true, successMessage, null);
                else return (true, null, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserEmployeeRoleAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/flipUserEmployeeRole?id={id}"; 
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string successMessage = await response.Content.ReadAsStringAsync();
                if (successMessage != null && successMessage.Length > 0) return (true, successMessage, null);
                else return (true, null, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserCustomerRoleAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/flipUserCustomerRole?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string successMessage = await response.Content.ReadAsStringAsync();
                if (successMessage != null && successMessage.Length > 0) return (true, successMessage, null);
                else return (true, null, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsLocked, string? ErrorMessage)> UserIsLockedAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/isUserLocked?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string isLocked = await response.Content.ReadAsStringAsync();
                if (isLocked == "true") return (true, null);
                else return (false, "User is not locked");
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to manage user.");
            }
        }

        public async Task<(bool IsUnlocked, string? ErrorMessage)> UserIsUnlockedAsync(string id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_AdminUsersPath}/isUserUnlocked?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string isUnlocked = await response.Content.ReadAsStringAsync();
                if (isUnlocked == "true") return (true, null);
                else return (false, "User is locked");
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to manage users.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to manage user.");
            }
        }
    }
}
