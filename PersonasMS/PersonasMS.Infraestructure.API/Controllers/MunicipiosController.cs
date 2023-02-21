﻿using Microsoft.AspNetCore.Mvc;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using System.Net.Mime;
using System.Net;
using PersonasMS.Domain.Dto.Common;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonasMS.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly ILogger<MunicipiosController> _logger;
        private readonly IBaseService<MunicipioCreateDto, MunicipioDto> _service;
        private readonly IMapper _mapper;

        public MunicipiosController(ILogger<MunicipiosController> logger, IBaseService<MunicipioCreateDto, MunicipioDto> service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        // POST api/<MunicipiosController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Municipio))]
        public IActionResult Post([FromBody] MunicipioCreateDto request)
        {
            _logger.LogInformation(nameof(MunicipiosController));
            _logger.LogInformation(nameof(Post));

            request.UsuarioCreacion = HttpContext.User.Identity?.Name;
            if (request.UsuarioCreacion is null)
                request.UsuarioCreacion = "System";

            request.FechaCreacion = DateTime.Now;

            var(status, id) = _service.Post(request);

            _logger.LogInformation($"status: {status} , id: {id}");

            if (status)
            {
                return Ok(new ResponseObjectDto<int>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Status = status,
                    Message = "Municipio insertado con éxito.",
                    Data = status ? id : default
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<int>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Municipio no insertado con éxito.",
                    Data = status ? id : default
                });
            }
        }

        // PUT api/<MunicipiosController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseObjectDto<bool>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Municipio))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] MunicipioDto request)
        {
            _logger.LogInformation(nameof(MunicipiosController));
            _logger.LogInformation(nameof(PutAsync));

            if (id != request.Id)
                return BadRequest();

            if (request.UsuarioModificacion is null)
                request.UsuarioModificacion = "System";

            request.FechaModificacion = DateTime.Now;

            var status = await _service.PutAsync(request, id).ConfigureAwait(false);

            if (status)
            {
                return Ok(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Status = status,
                    Message = "Municipio actualizado con éxito.",
                    Data = status
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Municipio no actualizado con éxito.",
                    Data = status
                });
            }

        }

        // GET: api/<MunicipiosController>
        [HttpGet("{page:int}/{limit:int}")]
        [ProducesResponseType(typeof(ResponseObjectDto<IEnumerable<Municipio>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit)
        {
            _logger.LogInformation(nameof(MunicipiosController));
            _logger.LogInformation(nameof(GetAllAsync));
            
            var result = await _service.GetAllAsync(page ?? 1, limit ?? 1000, "Id").ConfigureAwait(false);
            var resultDto = result as MunicipioDto[] ?? result.ToArray();

            var response = new ResponseObjectDto<IEnumerable<MunicipioDto>>
            {
                HttpStatusCode = resultDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = resultDto.Any(),
                Message = resultDto.Any() ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = resultDto
            };

            return Ok(response);
        }

        // GET api/<MunicipiosController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<MunicipioDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.GetByIdAsync(id).ConfigureAwait(false);
            var existResult = result != null;
            var response = new ResponseObjectDto<MunicipioDto>
            {
                HttpStatusCode = existResult ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = existResult,
                Message = existResult ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = result
            };
            return Ok(response);
        }

        // DELETE api/<MunicipiosController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseObjectDto<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation(nameof(DeleteAsync));

            var status = await _service.DeleteAsync(id).ConfigureAwait(false);

            var response = new ResponseObjectDto<bool>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Status = status,
                Message = "Asignación eliminada con éxito.",
                Data = true
            };
            return Ok(response);
        }
    }
}
