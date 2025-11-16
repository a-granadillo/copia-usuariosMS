using MediatR;
using Microsoft.AspNetCore.Mvc;
using Usuario_Aplicacion.Commands;

namespace Usuario_Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] CrearUsuarioCommand command)
        {
            var usuarioDto = await _mediator.Send(command);
            return CreatedAtAction(nameof(CrearUsuario), new { id = usuarioDto.Id }, usuarioDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(string id, [FromBody] ActualizarUsuarioCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("El id de la ruta no coincide con el del cuerpo de la solicitud.");
            }
            try
            {
                var usuarioDto = await _mediator.Send(command);
                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            try
            {
                var command = new EliminarUsuarioCommand(id);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
