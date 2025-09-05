using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;
using System.Net.Http.Headers;

namespace PetHealthcare.MVC.HttpProviders
{
    public class PetHttpProvider : IPetHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PetHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<PetDTO>? Pets, string? ErrorMessage)> GetAllPetsAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_PetPath}/getAllPets";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri + "/GetAllPets");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PetDTO>? pets = await response.Content.ReadFromJsonAsync<IEnumerable<PetDTO>>();
                return (true, pets, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view pets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to retrieve the pets.");
            }

        }

        public async Task<(bool IsSuccess, PetDTO? Pet, string? ErrorMessage)> GetPetAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_PetPath}/getPet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri + $"/GetPet?id={id}");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                PetDTO? pet = await response.Content.ReadFromJsonAsync<PetDTO>();
                return (true, pet, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view a pet.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to retrieve the pet.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddPetAsync(PetDTO petDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_PetPath}/addPet";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri + $"/AddPet");
            //request.Content = new StringContent(JsonSerializer.Serialize(petDTO), Encoding.UTF8, "application/json");
            using (var content = new MultipartFormDataContent())
            {
                //no Id for add
                if (petDTO.Name != null) content.Add(new StringContent(petDTO.Name!), nameof(petDTO.Name));
                if (petDTO.Nickname != null) content.Add(new StringContent(petDTO.Nickname!), nameof(petDTO.Nickname));
                if (petDTO.Gender != null) content.Add(new StringContent(petDTO.Gender!), nameof(petDTO.Gender));
                if (petDTO.Breed != null) content.Add(new StringContent(petDTO.Breed!), nameof(petDTO.Breed));
                if (petDTO.DateOfBirth != null) content.Add(new StringContent(petDTO.DateOfBirth.ToString()!), nameof(petDTO.DateOfBirth));
                if (petDTO.DateOfAdoption != null) content.Add(new StringContent(petDTO.DateOfAdoption.ToString()!), nameof(petDTO.DateOfAdoption));
                if (petDTO.ChipNumber != null) content.Add(new StringContent(petDTO.ChipNumber!), nameof(petDTO.ChipNumber));
                if (petDTO.Allergies != null) content.Add(new StringContent(petDTO.Allergies!), nameof(petDTO.Allergies));
                //content.Add(new StringContent(petDTO.ImageURL!), nameof(petDTO.ImageURL));
                if (petDTO.Image == null)
                {
                    request.Content = content;
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode) return (true, null);
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add pets.");
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                        else return (false, "Unknown error trying to add the pet.");
                    }
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await petDTO.Image.CopyToAsync(memoryStream);
                        using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        content.Add(fileContent, "petDTO.Image", petDTO.Image.FileName);
                        request.Content = content;

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode) return (true, null);
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add pets.");
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                            else return (false, "Unknown error trying to add the pet.");
                        }
                    }
                }
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditPetAsync(int id, PetDTO petDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_PetPath}/editPet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri + $"/EditPet?id={id}");

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(petDTO.Id.ToString()!), nameof(petDTO.Id));
                if (petDTO.Name != null) content.Add(new StringContent(petDTO.Name!), nameof(petDTO.Name));
                if (petDTO.Nickname != null) content.Add(new StringContent(petDTO.Nickname!), nameof(petDTO.Nickname));
                if (petDTO.Gender != null) content.Add(new StringContent(petDTO.Gender!), nameof(petDTO.Gender));
                if (petDTO.Breed != null) content.Add(new StringContent(petDTO.Breed!), nameof(petDTO.Breed));
                if (petDTO.DateOfBirth != null) content.Add(new StringContent(petDTO.DateOfBirth.ToString()!), nameof(petDTO.DateOfBirth));
                if (petDTO.DateOfAdoption != null) content.Add(new StringContent(petDTO.DateOfAdoption.ToString()!), nameof(petDTO.DateOfAdoption));
                if (petDTO.ChipNumber != null) content.Add(new StringContent(petDTO.ChipNumber!), nameof(petDTO.ChipNumber));
                if (petDTO.Allergies != null) content.Add(new StringContent(petDTO.Allergies!), nameof(petDTO.Allergies));
                if (petDTO.ImageFileName != null) content.Add(new StringContent(petDTO.ImageFileName!), nameof(petDTO.ImageFileName));
                //content.Add(new StringContent(petDTO.ImageURL!), nameof(petDTO.ImageURL));
                if (petDTO.Image == null)
                {
                    request.Content = content;
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode) return (true, null);
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit pets.");
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                        else return (false, "Unknown error trying to edit the pet.");
                    }
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await petDTO.Image.CopyToAsync(memoryStream);
                        using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        content.Add(fileContent, "petDTO.Image", petDTO.Image.FileName);
                        request.Content = content;

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode) return (true, null);
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit pets.");
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                            else return (false, "Unknown error trying to edit the pet.");
                        }
                    }
                }
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeletePetAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_PetPath}/deletePet?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri + $"/DeletePet?id={id}");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode) return (true, null);
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) return (false, "You are not authorized to delete pets.");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to delete pets.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to delete the pet.");
            }
        }
    }
}
