using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Utility;
using System.Net.Http.Headers;

namespace PetHealthcare.MVC.HttpProviders
{
    public class CarouselImagesHttpProvider : ICarouselImagesHttpProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarouselImagesHttpProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<CarouselImageDTO>? Images, string? ErrorMessage)> GetAllCarouselImagesAsync(string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_CarouselImagesPath}/getAllCarouselImages";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<CarouselImageDTO>? images = await response.Content.ReadFromJsonAsync<IEnumerable<CarouselImageDTO>>();
                return (true, images, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view carousel images.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to retrieve the images.");
            }

        }

        public async Task<(bool IsSuccess, CarouselImageDTO? Image, string? ErrorMessage)> GetCarouselImageAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_CarouselImagesPath}/getCarouselImage?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                CarouselImageDTO? image = await response.Content.ReadFromJsonAsync<CarouselImageDTO>();
                return (true, image, null);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, null, "You are not authorized to view an image.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, null, errorMessage);
                else return (false, null, "Unknown error trying to retrieve the image.");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddCarouselImageAsync(CarouselImageDTO imageDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_CarouselImagesPath}/addCarouselImage";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
            //request.Content = new StringContent(JsonSerializer.Serialize(imageDTO), Encoding.UTF8, "application/json");
            using (var content = new MultipartFormDataContent())
            {
                //no Id for add
                if (imageDTO.Subject != null) content.Add(new StringContent(imageDTO.Subject!), nameof(imageDTO.Subject));
                if (imageDTO.Caption != null) content.Add(new StringContent(imageDTO.Caption!), nameof(imageDTO.Caption));
                if (imageDTO.ImageFileName != null) content.Add(new StringContent(imageDTO.ImageFileName!), nameof(imageDTO.ImageFileName));
                //content.Add(new StringContent(imageDTO.ImageURL!), nameof(imageDTO.ImageURL));
                if (imageDTO.Image == null)
                {
                    request.Content = content;
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode) return (true, null);
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add images.");
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                        else return (false, "Unknown error trying to add the image.");
                    }
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageDTO.Image.CopyToAsync(memoryStream);
                        using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        content.Add(fileContent, "imageDTO.Image", imageDTO.Image.FileName);
                        request.Content = content;

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode) return (true, null);
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to add images.");
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                            else return (false, "Unknown error trying to add the image.");
                        }
                    }
                }
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditCarouselImageAsync(int id, CarouselImageDTO imageDTO, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_CarouselImagesPath}/editCarouselImage?id={id}";
            var client = _httpClientFactory.CreateClient(StaticDetails.PetHealthcareApi_ClientName);
            if (token != null && token.Length > 0)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(imageDTO.Id.ToString()!), nameof(imageDTO.Id));
                if (imageDTO.Subject != null) content.Add(new StringContent(imageDTO.Subject!), nameof(imageDTO.Subject));
                if (imageDTO.Caption != null) content.Add(new StringContent(imageDTO.Caption!), nameof(imageDTO.Caption));
                //content.Add(new StringContent(imageDTO.ImageURL!), nameof(imageDTO.ImageURL));
                if (imageDTO.Image == null)
                {
                    request.Content = content;
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode) return (true, null);
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit images.");
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                        else return (false, "Unknown error trying to edit the image.");
                    }
                }
                else
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageDTO.Image.CopyToAsync(memoryStream);
                        using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        content.Add(fileContent, "imageDTO.Image", imageDTO.Image.FileName);
                        request.Content = content;

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode) return (true, null);
                        else
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to edit images.");
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                            else return (false, "Unknown error trying to edit the image.");
                        }
                    }
                }
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteCarouselImageAsync(int id, string token = "")
        {
            string uri = $"{StaticDetails.PetHealthcareApi_CarouselImagesPath}/deleteCarouselImage?id={id}";
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
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) return (false, "You are not authorized to delete images.");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return (false, "You are not authorized to delete images.");
                string errorMessage = await response.Content.ReadAsStringAsync();
                if (errorMessage != null && errorMessage.Length > 0) return (false, errorMessage);
                else return (false, "Unknown error trying to delete the image.");
            }
        }
    }
}
