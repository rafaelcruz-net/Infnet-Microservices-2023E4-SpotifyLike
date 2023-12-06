using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;

namespace SpotifyLike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _service = new UsuarioService();

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> CriarConta(UsuarioDto dto)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            await this._service.CriarConta(dto);

            return Created($"/usuario/{dto.Id}", dto);
        }

        [HttpGet("{id}")]
        public IActionResult ObterUsuario(Guid id)
        {
            var result = this._service.ObterUsuario(id);
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("{id}/favoritar")]
        public async Task<IActionResult> FavoritarMusica(Guid id, FavoritarDto dto)
        {
            await this._service.FavoritarMusica(id, dto.IdMusica);
            return Ok();
        }

    }
}
