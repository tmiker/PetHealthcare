using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace PetHealthcare.MVC.HttpProviders
{
    public class VetHttpProvider : IVetHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VetHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<VetDTO>? Vets, string? ErrorMessage)> GetAllVetsAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VetPath}/getAllVets";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri + "/GetAllVets");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<VetDTO>? vets = await response.Content.ReadFromJsonAsync<IEnumerable<VetDTO>>();
                return (true, vets, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view vets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the vets.");
            }

        }

        public async Task<(bool IsSuccess, VetDTO? Vet, string? ErrorMessage)> GetVetAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VetPath}/getVet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri + $"/GetVet?id={id}");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                VetDTO? vet = await response.Content.ReadFromJsonAsync<VetDTO>();
                return (true, vet, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view a vet.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the vet.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddVetAsync(VetDTO vetDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VetPath}/addVet";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri + $"/AddVet");
            request.Content = new StringContent(JsonSerializer.Serialize(vetDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add vets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to add the vet.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditVetAsync(int id, VetDTO vetDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VetPath}/editVet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri + $"/EditVet?id={vetDTO.Id}");
            request.Content = new StringContent(JsonSerializer.Serialize(vetDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit vets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to edit the vet.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteVetAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VetPath}/deleteVet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri + $"/DeleteVet?id={id}");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) return (false, "You are not authorized to delete vets.");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to delete vets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to delete the vet.");
            }
        }
    }
}
