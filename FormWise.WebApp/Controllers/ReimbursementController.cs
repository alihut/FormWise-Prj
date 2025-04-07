using FormWise.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FormWise.WebApp.Controllers
{
    public class ReimbursementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ReimbursementController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new ReimbursementFormModel { Date = DateTime.Today });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReimbursementFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            if (model.Amount <= 0)
            {
                return BadRequest("Amount should be greater than 0");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(model.Date.ToString("o")), "Date"); // ISO 8601 format
            form.Add(new StringContent(model.Amount.ToString()), "Amount");
            form.Add(new StringContent(model.Description), "Description");

            using var stream = model.Receipt.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(model.Receipt.ContentType);
            form.Add(fileContent, "Receipt", model.Receipt.FileName);

            var apiSettings = _configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();

            var response = await client.PostAsync($"{apiSettings.ApiUrl}/reimbursements", form);

            if (response.IsSuccessStatusCode)
            {
                model = new ReimbursementFormModel
                {
                    Date = DateTime.Today,
                    SuccessMessage = "Reimbursement submitted successfully!"
                };
                return View(model);
            }

            model.ErrorMessage = "Failed to submit reimbursement. Please try again.";
            return View(model);
        }
    }
}
