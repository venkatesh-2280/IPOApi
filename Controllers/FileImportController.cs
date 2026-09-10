using IPOApi.STADataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
//using IPOApi.Models;
using static IPOApi.Models.UtilityModel;

namespace IPOApi.Controllers
{
    public class FileImportController : Controller
    {
        private IConfiguration _configuration;
        public FileImportData objData = new FileImportData();
        public FileImportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string constring = "";


        [HttpPost("BankDetails")]
        public IActionResult BankDetails()
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            //headerValue header_value = new headerValue();
            DataSet response = new DataSet();
            try
            {
                response = objData.getBank(constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpPost("fileinfo")]
        public IActionResult fileinfo([FromBody] FileInfoRequest fileInfo)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {
                response = objData.getJobinfo(fileInfo, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("getdatasetPipeline")]
        public IActionResult getdatasetPipeline(string Pipeline_code)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {
                response = objData.getdatasetPipelineData(Pipeline_code, constring);
                var serializedProduct = JsonConvert.SerializeObject(response, Formatting.None);
                return Ok(serializedProduct);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message);
            }
        }

        [HttpGet("gettotal")]
        public IActionResult gettotal(string ipocodes)
        {
            constring = _configuration.GetSection("Appsettings")["ConnectionStrings"].ToString();
            DataSet response = new DataSet();
            try
            {
                response = objData.getdatasetTotalData(ipocodes, constring);
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
