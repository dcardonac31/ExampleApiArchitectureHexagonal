using Microsoft.AspNetCore.Mvc;
using PersonasMS.Application.Interfaces;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;
using System.Net.Mime;
using System.Net;
using PersonasMS.Domain.Dto.Common;
using AutoMapper;
using NPOI.XSSF.UserModel;
using PersonasMS.Infraestructure.API.Validations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonasMS.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ILogger<PersonasController> _logger;
        private readonly IBaseService<PersonaCreateDto, PersonaDto> _service;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public PersonasController(ILogger<PersonasController> logger, IBaseService<PersonaCreateDto, PersonaDto> service, IPersonaService personaService, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _personaService = personaService;
            _mapper = mapper;
        }

        // POST api/<PersonasController>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Persona))]
        public IActionResult Post([FromBody] PersonaCreateDto request)
        {
            _logger.LogInformation(nameof(PersonasController));
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
                    Message = "Persona insertada con éxito.",
                    Data = status ? id : default
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<int>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Persona no insertada con éxito.",
                    Data = status ? id : default
                });
            }
        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseObjectDto<bool>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Persona))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] PersonaDto request)
        {
            _logger.LogInformation(nameof(PersonasController));
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
                    Message = "Persona actualizada con éxito.",
                    Data = status
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Persona no actualizada con éxito.",
                    Data = status
                });
            }

        }

        // GET: api/<PersonasController>
        [HttpGet("{page:int}/{limit:int}")]
        [ProducesResponseType(typeof(ResponseObjectDto<IEnumerable<Persona>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit)
        {
            _logger.LogInformation(nameof(PersonasController));
            _logger.LogInformation(nameof(GetAllAsync));
            
            var result = await _service.GetAllAsync(page ?? 1, limit ?? 1000, "Id").ConfigureAwait(false);
            var resultDto = result as PersonaDto[] ?? result.ToArray();

            var response = new ResponseObjectDto<IEnumerable<PersonaDto>>
            {
                HttpStatusCode = resultDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = resultDto.Any(),
                Message = resultDto.Any() ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = resultDto
            };

            return Ok(response);
        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<PersonaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.GetByIdAsync(id).ConfigureAwait(false);
            var existResult = result != null;
            var response = new ResponseObjectDto<PersonaDto>
            {
                HttpStatusCode = existResult ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = existResult,
                Message = existResult ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = result
            };
            return Ok(response);
        }

        // GET api/<PersonasController>/GetByCedulaAsync/123456789
        [HttpGet("GetByCedulaAsync/{cedula}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<PersonaDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCedulaAsync(string cedula)
        {
            _logger.LogInformation(nameof(GetByCedulaAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _personaService.GetByCedulaAsync(cedula).ConfigureAwait(false);
            var existResult = result != null;
            var response = new ResponseObjectDto<PersonaDto>
            {
                HttpStatusCode = existResult ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                Status = existResult,
                Message = existResult ? HttpStatusCode.OK.ToString() : HttpStatusCode.NoContent.ToString(),
                Data = result
            };
            return Ok(response);
        }

        // DELETE api/<PersonasController>/5
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

        // PUT api/<PersonasController>/inactivar-persona/5
        [HttpPut("inactivar-persona/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseObjectDto<bool>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Persona))]
        public async Task<IActionResult> InactivarPersonaAsync(int id, [FromBody] PersonaDto request)
        {
            _logger.LogInformation(nameof(PersonasController));
            _logger.LogInformation(nameof(InactivarPersonaAsync));

            if (id != request.Id)
                return BadRequest();

            request.Activo = false;

            if (request.UsuarioModificacion is null)
                request.UsuarioModificacion = "System";

            request.FechaModificacion = DateTime.Now;

            var status = await _personaService.InactivarAsync(request, id).ConfigureAwait(false);

            if (status)
            {
                return Ok(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Status = status,
                    Message = "Persona inactividada con éxito.",
                    Data = status
                });
            }
            else
            {
                return BadRequest(new ResponseObjectDto<bool>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Status = status,
                    Message = "Persona inactividada sin éxito.",
                    Data = status
                });
            }

        }

        // POST api/InserDataExcelPersona<PersonasController>
        [HttpPost("insert-data-excel-persona")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseObjectDto<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(Persona))]
        public async Task<IActionResult> InsertDataExcelPersona([FromForm] IFormFile filePath)
        {
            _logger.LogInformation(nameof(PersonasController));
            _logger.LogInformation(nameof(InsertDataExcelPersona));

            using (var file = new FileStream(filePath.FileName, FileMode.Create)) 
            {
                await filePath.CopyToAsync(file);
            }

            using(var file = new FileStream(filePath.FileName, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(file);
                var sheet = workbook.GetSheetAt(0);
                int contRows = 0;
                bool statusTransaction = false;

                for (var i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    var rowInitial = 1;
                    if(i >= rowInitial)
                    {
                        var row = sheet.GetRow(i);

                        // Get row data

                        var cedula = ExcelValidation.GetCellValue<string>(row.GetCell(0), string.Empty);
                        var primerNombre = ExcelValidation.GetCellValue<string>(row.GetCell(1), string.Empty);
                        var segundoNombre = ExcelValidation.GetCellValue<string>(row.GetCell(2), string.Empty);
                        var primerApellido = ExcelValidation.GetCellValue<string>(row.GetCell(3), string.Empty);
                        var segundoApellido = ExcelValidation.GetCellValue<string>(row.GetCell(4), string.Empty);
                        var fechaNacimiento = ExcelValidation.GetCellValue<DateTime>(row.GetCell(5), new DateTime(1900, 1, 1));
                        var generoId = ExcelValidation.GetCellValue<int>(row.GetCell(6), 1);
                        var cargoId = ExcelValidation.GetCellValue<int>(row.GetCell(7), 1);
                        var municipioNacimientoId = ExcelValidation.GetCellValue<int>(row.GetCell(8), 1);
                        var municipioResidenciaId = ExcelValidation.GetCellValue<int>(row.GetCell(9), 1);
                        var direccion = ExcelValidation.GetCellValue<string>(row.GetCell(10), "Calle 12 # 30 – 80 (CASA SOFKA MEDELLÍN)");
                        var activo = ExcelValidation.GetCellValue<bool>(row.GetCell(11), true);

                        var request = new PersonaCreateDto();

                        request.Cedula = cedula;
                        request.PrimerNombre = primerNombre;
                        request.SegundoNombre = segundoNombre;
                        request.PrimerApellido = primerApellido;
                        request.SegundoApellido = segundoApellido;
                        request.FechaNacimiento = fechaNacimiento;
                        request.GeneroId = generoId;
                        request.CargoId = cargoId;
                        request.MunicipioNacimientoId = municipioNacimientoId;
                        request.MunicipioResidenciaId = municipioResidenciaId;
                        request.Direccion = direccion;
                        request.Activo = activo;

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


                if (contRows > 0)
                {
                    return Ok(new ResponseObjectDto<int>
                    {
                        HttpStatusCode = HttpStatusCode.OK,
                        Status = statusTransaction,
                        Message = $"Cantidad de registros de Persona insertados con éxito: {contRows}",
                        Data = contRows
                    });
                }
                else
                {
                    return BadRequest(new ResponseObjectDto<int>
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Status = false,
                        Message = "Error en carga de archivo de Personas.",
                        Data = contRows
                    });
                }
            }
        }
    }
}
