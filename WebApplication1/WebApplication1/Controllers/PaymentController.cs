using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using System.Text.RegularExpressions;
using Refit;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post(PaymentDTO payments)
        {

            Regex regexMC = new Regex(@"^5[1-5][0-9]{14}$");
            Regex regexVisa = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regAmEx = new Regex(@"^3[47][0-9]{13}$");
            Regex regCVCMV = new Regex ("^[0-9]{3}$");
            Regex regCVCAmEx = new Regex("^[0-9]{4}$");
        
            if (regexMC.Match(payments.CCNumber).Success==true){
              return Ok("Type Master Card");
            }

            else if (regexVisa.Match(payments.CCNumber).Success == true)
            {
              return Ok("Type Visa");
            }

            else if (regAmEx.Match(payments.CCNumber).Success == true)
            {
              return Ok("Type American Express");
            }

            else
            {
                return Ok("No Type");
            }

        } 

    }

}
