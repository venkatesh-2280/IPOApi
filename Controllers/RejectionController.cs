using IPOApi.Models;
using IPOApi.Services; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
//using static IPOApi.Models.UserManagementModel;

namespace IPOApi.Controllers
{
    public class RejectionController : Controller
    {
        private IConfiguration _configuration;
        public RejectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";

        [HttpGet("getRejReason")]
        public IActionResult getRejReason(string offer_code, bool runRule)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataTable response = new DataTable();
            try
            {
                response = RejectionService.GetRejService(offer_code, runRule, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpGet("GetRejectiondetail")]
        public IActionResult GetRejectiondetail(string offer_code, string rule_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataTable response = new DataTable();
            try
            {
                response = RejectionService.GetRejdetailService(offer_code, rule_code, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        // runRejection
        [HttpGet("runRejection")]
        public IActionResult runRejection(string offer_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataTable response = new DataTable();
            try
            {
                response = RejectionService.runRejectionService(offer_code, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpGet("GetAddRejList")]
        public IActionResult GetAddRejList(string ipo_code,
                                     string appl_no,
                                     string order_no,
                                     string pan_no,
                                     string flag)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {
                response = RejectionService.GetAddRejService(
                                                               ipo_code,
                                                               appl_no,
                                                               order_no,
                                                               pan_no,
                                                               flag,
                                                               constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }
        [HttpPost("saveAddRejDetails")]
        public IActionResult saveAddRejDetails([FromBody] RejectionModel insObj)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                var getvalue = Request.Headers.TryGetValue("user_code", out var user_code) ? user_code.First() : "";
                header_value.user_code = getvalue;
                response = RejectionService.saveaddrejdetails(insObj, header_value, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }

        }

        [HttpPost("Getrulecode")]
        public IActionResult Getrulecode(string ipo_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {
                response = RejectionService.Getrulecode(constring, ipo_code);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
                //return Ok(response.Tables[0]);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("InsRightsEntitlement")]
        public IActionResult InsRightsEntitlement(string offer_code)
        {
            constring = _configuration
                .GetSection("Appsettings")["ConnectionStrings"]
                .ToString();

            try
            {
                DataTable response = RejectionService
            .InsertRightsEntitlement(offer_code, constring);

                string result = "";

                if (response != null &&
                    response.Rows.Count > 0 &&
                    response.Columns.Contains("result"))
                {
                    result = response.Rows[0]["result"].ToString();
                }

                return Ok(new
                {
                    result = result
                });
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("GetRightsEntitlement")]
        public IActionResult GetRightsEntitlement(string offer_code)
        {
            constring = _configuration
                .GetSection("Appsettings")["ConnectionStrings"]
                .ToString();

            try
            {
                DataSet ds = RejectionService.GetRightsEntitlement(
                    offer_code,
                    constring
                );

                var result = new
                {
                    summary = ds.Tables[0].AsEnumerable()
                        .Select(row => row.ItemArray)
                        .ToList(),

                    details = ds.Tables[1].AsEnumerable()
                        .Select(row => row.ItemArray)
                        .ToList(),

                    disable_flag = ds.Tables[2].AsEnumerable()
                        .Select(row => row.ItemArray)
                        .ToList()
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

    }
}
