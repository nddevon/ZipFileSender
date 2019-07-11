using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZIP.Utility;

namespace ZIP.FileSender.Controllers {
    public class BaseController : Controller
    {
        public Task<HttpResponseMessage> ApiJsonRequest(string apiUrl, string contentJson) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:59279/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var userName = "admin";//            HttpContext.Session.GetString("UserName");
            var pasword = "123";// HttpContext.Session.GetString("Password");

            string userInfo = string.Format("{0}:{1}", userName, pasword);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", userInfo.Base64Encode());
            var httpContent = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(apiUrl, httpContent);
            return response;
        }
    }
}