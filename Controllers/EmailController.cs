using IPOApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace IPOApi.Controllers
{
    public class EmailController : Controller
    {
        private IConfiguration _configuration;
        private readonly EmailService _service;
        public EmailController(IConfiguration configuration, EmailService service)
        {
            _configuration = configuration;
            _service = service;
        }
        string constring = "";

        [HttpGet("sendIPOMails")]
        public async Task<IActionResult> SendIpoEmails(string offer_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
           // DataTable response = new DataTable();
            try
            {
                var response = await _service.ProcessBulkEmails(offer_code, constring);
                return Ok(new
                {
                    status = "success",
                    message = response
                });
                //var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                //return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("getemailList")]
        public IActionResult getemailList(string offer_code, string in_action)
        {
            DataSet response = new DataSet();
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            try
            {
                response = _service.getemailListService(offer_code, in_action, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("Getbidfilecountsummary")]
        public IActionResult Getbidfilecountsummary(string ipo_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {               
                response = _service.GetbidfilecountsummaryService(ipo_code, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

    }
}
