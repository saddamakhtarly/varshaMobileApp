using MobileAppAPI.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class HttpRequest
    {
        public async Task<bool> LoginUser(LoginRequest rqst)
        {
            bool resp = false;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://192.168.43.254:82/api/User/Login"))
                {

                    string json = JsonConvert.SerializeObject(rqst);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                    string jsonData = await response.Content.ReadAsStringAsync();
                    LoginResponse userResp = JsonConvert.DeserializeObject<LoginResponse>(jsonData);
                    resp = userResp.IsValid;
                }
            }
            return resp;
        }

        public async Task<LessonsResponse> GetLesson()
        {
            LessonsResponse resp = new LessonsResponse();
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "http://192.168.43.254:82/api/User/GetLessonsData"))
                {
                    request.Content = new StringContent("");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    string jsonData = await response.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<LessonsResponse>(jsonData);
                }
            }
            return resp;
        }

        public async Task<MusicResponse> GetMusic(int lessonId)
        {
            MusicResponse resp = new MusicResponse();
            var handler = new HttpClientHandler();
            handler.UseCookies = false;

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://192.168.43.254:82/api/User/GetMusicsData?LessonId={lessonId}"))
                {
                    request.Content = new StringContent("");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    string jsonData = await response.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<MusicResponse>(jsonData);
                }
            }
            return resp;
        }

        public async Task<SaveImageResponse> SaveImage(List<MediaFile> images)
        {
            SaveImageResponse resp = new SaveImageResponse();
            var handler = new HttpClientHandler();
            handler.UseCookies = false;
            using (var httpClient = new HttpClient(handler))
            {
                string url = "http://192.168.43.254:82/api/User/SaveImage";
                var content = new MultipartFormDataContent();
                foreach (var file in images)
                {
                    content.Add(new StreamContent(file.GetStream()), "\"file\"", Path.GetFileName(file.Path));
                }
                var httpResponseMessage = await httpClient.PostAsync(url, content);
                var jsonDataresp = await httpResponseMessage.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<SaveImageResponse>(jsonDataresp);
            }
            return resp;
        }
    }
}
