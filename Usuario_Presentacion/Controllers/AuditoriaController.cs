using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Usuario_Aplicacion.Commands;
using Usuario_Aplicacion.DTOs;

namespace Usuario_Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // La ruta será: /api/auditoria
    [Authorize(Roles = "Administrador,Soporte")]
    public class AuditoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta los logs de auditoría filtrados por módulo.
        /// </summary>
        /// <param name="modulo">El nombre del módulo (ej. "SEGURIDAD", "PAGOS", "USUARIOS").</param>
        /// <returns>Lista de eventos de auditoría.</returns>
        [HttpGet("{modulo}")]
        public async Task<IActionResult> ObtenerPorModulo(string modulo)
        {
            try
            {
                var query = new ObtenerAuditoriaModuloCommand(modulo);
                var resultado = await _mediator.Send(query);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno consultando auditoría: {ex.Message}");
            }
        }
    }
}
