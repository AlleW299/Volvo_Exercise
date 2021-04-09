using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo_CTC.CommonFunctions;
using Newtonsoft.Json;
using Volvo_CTC.Models;

namespace Volvo_CTC.Controllers
{
    /// <summary>
    /// Surfable through https://localhost:44328/swagger/index.html
    /// Time permitting:
    /// JWT security would be implmented.
    /// Logging would be fully implemented.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxCalculatorController : ControllerBase
    {
        private readonly ILogger<CongestionTaxCalculatorController> _logger;

        public CongestionTaxCalculatorController(ILogger<CongestionTaxCalculatorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GetTax
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///        "Vehicle": "car",
        ///         "Dates": [
        ///            "2013-02-07 06:23:27",
        ///            "2013-02-07 15:27:00"
        ///         ]
        ///     }
        /// </remarks>
        /// <response code="200">{\"TotalTax\":13}</response>
        /// <response code="404">Error: Not Found</response>
        [HttpPost]
        public async Task<IActionResult> GetTax([FromBody] string rawJsonOject)
        {
            try
            {
                var tax = JsonConvert.DeserializeObject<Tax>(rawJsonOject);

                Vehicle vehicle = null;
                switch (tax.Vehicle.ToLower())
                {
                    case "car":
                        vehicle = new Car();
                        break;
                    case "motorcycle":
                        vehicle = new Motorcycle();
                        break;
                    default:
                        return NotFound();
                        break;
                }

                var response = new TaxResponse();
                response.TotalTax = new CongestionTaxCalculator().GetTax(vehicle, tax.Dates);
                return Ok(JsonConvert.SerializeObject(response));
            }
            catch
            {
                return NotFound();
            }
        }

    }
}



