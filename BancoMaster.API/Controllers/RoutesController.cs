using BancoMaster.Domain.Interfaces;
using BancoMaster.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BancoMaster.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        public IRoutes RoutesRepo;
        private readonly ILogger<RoutesController> _logger;

        public RoutesController(ILogger<RoutesController> logger, IRoutes RoutesRepo)
        {
            _logger = logger;
            this.RoutesRepo = RoutesRepo;
        }

        /// <summary>
        /// Carrega rotas especificas a partir da abreviatura de origem e destino
        /// </summary>
        /// <param name="originAbbr"></param>
        /// <param name="destinyAbbr"></param>
        /// <returns></returns>
        [HttpGet("{originAbbr}/{destinyAbbr}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string originAbbr, string destinyAbbr)
        {
            try
            {
                return Ok(await RoutesRepo.ListingValues(originAbbr, destinyAbbr));
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Carrega a rota com o melhor preço e busca por abreviatura de origem e destino
        /// </summary>
        /// <param name="originAbbr"></param>
        /// <param name="destinyAbbr"></param>
        /// <returns></returns>
        [HttpGet("best-price/{originAbbr}/{destinyAbbr}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BestPrice(string originAbbr, string destinyAbbr)
        {
            try
            {
                var result = await RoutesRepo.BestPrice(originAbbr, destinyAbbr);
                var st = "";
                var connections = ""; 
                if (result.Id != 0)
                {
                    result.Connections.ForEach(c =>  connections += " - " + c.AbbrName );
                    st = $"{result.OriginAbbr}{connections} - ao custo de ${result.Price}";
                }
                else
                    st = "Nenhum resultado encontrado";

                return Ok(st);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///    Edita as informações de uma rota já criada.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Routes route)
        {
            try
            {
                await RoutesRepo.Update(route);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///  Cria uma nova rota
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]Routes route)
        {
            try
            {
                await RoutesRepo.Add(route);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///  Deleta uma rota
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await RoutesRepo.Delete(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
