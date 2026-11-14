using Microsoft.AspNetCore.Mvc;
using Vendinha.Dtos;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Api.Controllers
{
    [ApiController]
    [Route("api/cliente/{clienteId:int}/divida")] //api/cliente/5/divida
    public class DividaController : ControllerBase
    {
        private readonly ClienteService clienteService;
        private readonly DividaService dividaService;

        public DividaController(ClienteService clienteService, DividaService dividaService)
        {
            this.clienteService = clienteService;
            this.dividaService = dividaService;
        }

        // GET api/cliente/5/divida
        [HttpGet]
        public IActionResult Listar(int clienteId)
        {
            if (!clienteService.Existe(clienteId)) { return NotFound(); }

            var dividas = dividaService.Listar(clienteId);
            return Ok(dividas.Select(DividaResponse.De));
        }

        // GET api/cliente/5/divida/12
        [HttpGet("{dividaId:int}")]
        public IActionResult Obter(int clienteId, int dividaId)
        {
            var divida = dividaService.Obter(clienteId, dividaId);
            if (divida is null) { return NotFound(); }

            return Ok(DividaResponse.De(divida));
        }

        // POST api/cliente/5/divida
        [HttpPost]
        public IActionResult Cadastrar(int clienteId, [FromBody] DividaRequest request)
        {
            if (!clienteService.Existe(clienteId)) { return NotFound(); }

            var divida = new Divida { Valor = request.Valor ?? 0 };

            var sucesso = dividaService.Criar(clienteId, divida, out var erros);
            if (!sucesso) { return UnprocessableEntity(ErroResponse.De(erros)); }

            return CreatedAtAction(nameof(Obter),
                new { clienteId, dividaId = divida.Id },
                DividaResponse.De(divida));
        }

        // POST api/cliente/5/divida/12/pagar
        [HttpPost("{dividaId:int}/pagar")]
        public IActionResult Pagar(int clienteId, int dividaId)
        {
            var divida = dividaService.Obter(clienteId, dividaId);
            if (divida is null) { return NotFound(); }

            var sucesso = dividaService.Pagar(divida, out var erros);
            if (!sucesso) { return UnprocessableEntity(ErroResponse.De(erros)); }

            return Ok(DividaResponse.De(divida));
        }
    }
}
