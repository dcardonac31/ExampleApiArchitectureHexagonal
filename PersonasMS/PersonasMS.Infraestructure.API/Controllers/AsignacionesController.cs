using Microsoft.AspNetCore.Mvc;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using System.Net.Mime;
using System.Net;
using PersonasMS.Domain.Dto.Common;
using AutoMapper;
using NPOI.XSSF.UserModel;
using PersonasMS.Application.Services;
using PersonasMS.Infraestructure.API.Validations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonasMS.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly ILogger<AsignacionesController> _logger;
        private readonly IBaseService<AsignacionCreateDto, AsignacionDto> _service;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public AsignacionesController(ILogger<AsignacionesController> logger, IBaseService<AsignacionCreateDto, AsignacionDto> service, IPersonaService personaService, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _personaService = personaService;
            _mapper = mapper;
        }

        // POST api/<AsignacionesController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Asignacion))]
        public IActionResult Post([FromBody] AsignacionCreateDto request)
        {
            _logger.LogInformation(nameof(AsignacionesController));
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
                    Message = "Asignación insertada con éxito.",
                    Data = status ? id : default
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<int>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Asignación no insertada con éxito.",
                    Data = status ? id : default
                });
            }
        }

        // PUT api/<AsignacionesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseObjectDto<bool>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Asignacion))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] AsignacionDto request)
        {
            _logger.LogInformation(nameof(AsignacionesController));
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
                    Message = "Asignación actualizada con éxito.",
                    Data = status
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Asignación no actualizada con éxito.",
                    Data = status
                });
            }


        }

        // GET: api/<AsignacionesController>
        [HttpGet("{page:int}/{limit:int}")]
        [ProducesResponseType(typeof(ResponseObjectDto<IEnumerable<Asignacion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit)
        {
            _logger.LogInformation(nameof(AsignacionesController));
            _logger.LogInformation(nameof(GetAllAsync));
            
            var result = await _service.GetAllAsync(page ?? 1, limit ?? 1000, "Id").ConfigureAwait(false);
            var resultDto = result as AsignacionDto[] ?? result.ToArray();

            var response = new ResponseObjectDto<IEnumerable<AsignacionDto>>
            {
                HttpStatusCode = resultDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = resultDto.Any(),
                Message = resultDto.Any() ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = resultDto
            };

            return Ok(response);
        }

        // GET api/<AsignacionesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<AsignacionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.GetByIdAsync(id).ConfigureAwait(false);
            var existResult = result != null;
            var response = new ResponseObjectDto<AsignacionDto>
            {
                HttpStatusCode = existResult ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = existResult,
                Message = existResult ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = result
            };
            return Ok(response);
        }

        // DELETE api/<AsignacionesController>/5
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

        [HttpPost("insert-data-excel-asignacion")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Asignacion))]
        public async Task<IActionResult> InsertDataExcelAsignacion([FromForm] IFormFile filePath)
        {
            _logger.LogInformation(nameof(AsignacionesController));
            _logger.LogInformation(nameof(InsertDataExcelAsignacion));

            using (var file = new FileStream(filePath.FileName, FileMode.Create))
            {
                await filePath.CopyToAsync(file);
            }

            using (var file = new FileStream(filePath.FileName, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(file);
                var sheet = workbook.GetSheetAt(0);
                int contRows = 0;
                bool statusTransaction = false;

                for (var i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    var rowInitial = 1;
                    if (i >= rowInitial)
                    {
                        var row = sheet.GetRow(i);

                        // Get row data

                        var cedula = ExcelValidation.GetCellValue<string>(row.GetCell(0), string.Empty);
                        var clienteId = ExcelValidation.GetCellValue<int>(row.GetCell(1), 1);
                        var cargoId = ExcelValidation.GetCellValue<int>(row.GetCell(2), 1);
                        var fechaAsignacion = ExcelValidation.GetCellValue<DateTime>(row.GetCell(3), new DateTime(1900, 1, 1));
                        var fechaDesasignacion = ExcelValidation.GetCellValue<DateTime>(row.GetCell(4), new DateTime(1900, 1, 1));

                        var request = new AsignacionCreateDto();

                        var persona = await _personaService.GetByCedulaAsync(cedula).ConfigureAwait(false);

                        if (persona != null)
                        {
                            request.PersonaId = persona.Id;
                            request.ClienteId = clienteId;
                            request.CargoId = cargoId;
                            request.FechaAsignacion = fechaAsignacion;
                            request.FechaDesasignacion = fechaDesasignacion;

                            request.UsuarioCreacion = HttpContext.User.Identity?.Name;

                            if (request.UsuarioCreacion is null)
                                request.UsuarioCreacion = "ExcelUpload";

                            request.FechaCreacion = DateTime.Now;

                            var (status, id) = _service.Post(request);

                            _logger.LogInformation($"status: {status} , id: {id}");

                            statusTransaction = status;

                            contRows++;
                        }
                    }
                }

                if (contRows > 0)
                {
                    return Ok(new ResponseObjectDto<int>
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        Status = statusTransaction,
                        Message = $"Cantidad de registros de Asignacion insertados con éxito: {contRows}",
                        Data = contRows
                    });
                }
                else
                {
                    return BadRequest(new ResponseObjectDto<int>
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Status = false,
                        Message = "Error en carga de archivo de Asignacion.",
                        Data = contRows
                    });
                }
            }
        }
    }
}
