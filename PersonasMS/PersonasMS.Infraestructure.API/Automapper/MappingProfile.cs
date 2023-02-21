using AutoMapper;
using PersonasMS.Domain.Dto;
using PersonasMS.Domain.Entities;

namespace PersonasMS.Infraestructure.API.Automapper
{
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Asignacion, AsignacionCreateDto>().ReverseMap();
                CreateMap<Asignacion, AsignacionDto>().ReverseMap();
                CreateMap<Cargo, CargoCreateDto>().ReverseMap();
                CreateMap<Cargo, CargoDto>().ReverseMap();
                CreateMap<Cliente, ClienteCreateDto>().ReverseMap();
                CreateMap<Cliente, ClienteDto>().ReverseMap();
                CreateMap<Departamento, DepartamentoCreateDto>().ReverseMap();
                CreateMap<Departamento, DepartamentoDto>().ReverseMap();
                CreateMap<HistoricoCargo, HistoricoCargoCreateDto>().ReverseMap();
                CreateMap<HistoricoCargo, HistoricoCargoDto>().ReverseMap();
                CreateMap<IngresoRetiro, IngresoRetiroCreateDto>().ReverseMap();
                CreateMap<IngresoRetiro, IngresoRetiroDto>().ReverseMap();
                CreateMap<Municipio, MunicipioCreateDto>().ReverseMap();
                CreateMap<Municipio, MunicipioDto>().ReverseMap();
                CreateMap<Pais, PaisCreateDto>().ReverseMap();
                CreateMap<Pais, PaisDto>().ReverseMap();
                CreateMap<Persona, PersonaCreateDto>().ReverseMap();
                CreateMap<Persona, PersonaDto>().ReverseMap();
                CreateMap<Seguimiento, SeguimientoCreateDto>().ReverseMap();
                CreateMap<Seguimiento, SeguimientoDto>().ReverseMap();
            }
        }
}
