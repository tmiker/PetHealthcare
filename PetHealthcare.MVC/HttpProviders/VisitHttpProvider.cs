using PetHealthcare.MVC.Abstractions;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;

namespace PetHealthcare.MVC.HttpProviders
{
    public class VisitHttpProvider : IVisitHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VisitHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<VisitDTO>? Visits, string? ErrorMessage)> GetAllVisitsAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/getAllVisits";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<VisitDTO>? visits = await response.Content.ReadFromJsonAsync<IEnumerable<VisitDTO>>();
                return (true, visits, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the visits.");
            }
        }

        // I'm getting the aggregate anyway but not returning the aggregate dto above
        public async Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage)> GetAllVisitAggregatesAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/getAllVisitAggregates"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<VisitAggregateDTO>? visits = await response.Content.ReadFromJsonAsync<IEnumerable<VisitAggregateDTO>>();
                return (true, visits, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the visits.");
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage)> GetAllVisitAggregatesByPetAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/getAllVisitAggregatesByPet?id={id}"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<VisitAggregateDTO>? visits = await response.Content.ReadFromJsonAsync<IEnumerable<VisitAggregateDTO>>();
                return (true, visits, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the visits.");
            }
        }

        public async Task<(bool IsSuccess, VisitDTO? Visit, string? ErrorMessage)> GetVisitAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/getVisit?id={id}"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                VisitDTO? visit = await response.Content.ReadFromJsonAsync<VisitDTO>();
                return (true, visit, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view a visit.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the visit.");
            }
        }

        // I'm getting the aggregate anyway but not returning the aggregate dto above
        public async Task<(bool IsSuccess, VisitAggregateDTO? VisitAggregate, string? ErrorMessage)> GetVisitAggregateAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/getVisitAggregate?id={id}"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                VisitAggregateDTO? visit = await response.Content.ReadFromJsonAsync<VisitAggregateDTO>();
                return (true, visit, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view a visit.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to get the visit.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddVisitAsync(VisitDTO visitDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/addVisit"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(visitDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to add the visit.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditVisitAsync(int id, VisitDTO visitDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/editVisit?id={id}"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(visitDTO), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to edit the visit.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteVisitAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_VisitPath}/deleteVisit?id={id}"; ;
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) return (false, "You are not authorized to delete visits.");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to delete visits.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to delete the visit.");
            }
        }
    }
}
