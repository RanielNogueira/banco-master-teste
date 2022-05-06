using BancoMaster.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BancoMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        public IDestinations DestinationsRepo;
        private readonly ILogger<DestinationsController> _logger;

        public DestinationsController(ILogger<DestinationsController> logger, IDestinations DestinationsRepo)
        {
            _logger = logger;
            this.DestinationsRepo = DestinationsRepo;
        }

        /// <summary>
        /// Lista todos os destinos disponíveis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await DestinationsRepo.ListingValues());
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
