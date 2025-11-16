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
        
        
    }
}
