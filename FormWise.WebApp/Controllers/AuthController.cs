using FormWise.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FormWise.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();
            var loginPayload = JsonConvert.SerializeObject(new
            {
                Email = model.Email,
                Password = model.Password
            });

            var content = new StringContent(loginPayload, Encoding.UTF8, "application/json");

            var apiSettings = _configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();

            var response = await client.PostAsync($"{apiSettings.ApiUrl}/auth/login", content); // replace with your API URL

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(json);

                string token = result.data.token;
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Add", "Reimbursement");
            }

            model.ErrorMessage = "Invalid credentials. Please try again.";
            return View(model);
        }
    }
}