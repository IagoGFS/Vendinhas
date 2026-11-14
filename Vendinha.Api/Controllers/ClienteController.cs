using Microsoft.AspNetCore.Mvc;
using Vendinha.Dtos;
using Vendinha.Models;
using Vendinha.Services;

namespace Vendinha.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/cliente
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService service;

        public ClienteController(ClienteService service)
        {
            this.service = service;
        }

        // GET api/cliente?pesquisa=maria&page=1
        [HttpGet]
        public IActionResult Listar(string pesquisa, int page = 1)
        {
            var resultado = service.Listar(pesquisa, page);
            return Ok(resultado);
        }

        // GET api/cliente/5
        [HttpGet("{id:int}")]
        public IActionResult Obter(int id)
        {
            var cliente = service.Obter(id);
            if (cliente is null) { return NotFound(); }

            return Ok(ClienteResponse.De(cliente));
        }

        // POST api/cliente
        [HttpPost]
        public IActionResult Cadastrar([FromBody] ClienteRequest request)
        {
            var cliente = Mapear(request);

            var sucesso = service.Criar(cliente, out var erros);
            if (!sucesso) { return UnprocessableEntity(ErroResponse.De(erros)); }

            return CreatedAtAction(nameof(Obter), new { id = cliente.Id }, ClienteResponse.De(cliente));
        }

        // PUT api/cliente/5
        [HttpPut("{id:int}")]
        public IActionResult Atualizar(int id, [FromBody] ClienteRequest request)
        {
            if (!service.Existe(id)) { return NotFound(); }

            var dados = Mapear(request);

            var sucesso = service.Atualizar(id, dados, out var erros);
            if (!sucesso) { return UnprocessableEntity(ErroResponse.De(erros)); }

            var atualizado = service.Obter(id);
            return Ok(ClienteResponse.De(atualizado));
        }

        // DELETE api/cliente/5
        [HttpDelete("{id:int}")]
        public IActionResult Excluir(int id)
        {
            var sucesso = service.Excluir(id);
            if (!sucesso) { return NotFound(); }

            return NoContent();
        }

        private static Cliente Mapear(ClienteRequest request)
        {
            return new Cliente
            {
                NomeCompleto = request.NomeCompleto ?? string.Empty,
                Cpf = request.Cpf ?? string.Empty,
                DataNascimento = request.DataNascimento ?? default,
                Email = request.Email
            };
        }
    }
}
