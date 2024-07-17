using Microsoft.AspNetCore.Mvc;
using RestEncriptionApp.Models.Data;
using RestEncriptionApp.Services;
using System.Text;

namespace RestEncriptionApp.Controllers
{
    public class EncrptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("[controller]/encript")]
        public IActionResult Inputusername([FromBody] DataEncryption dataEncryption)
        {
            EncrptionServices encrptionServices = new EncrptionServices();
            try
            {
               return Ok(new { ecript = encrptionServices.EncryprtData(dataEncryption.Param , dataEncryption.Salt, Encoding.UTF8.GetBytes(dataEncryption.Key)), salt = dataEncryption.Salt, Data ="Success"});
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

    }
}
