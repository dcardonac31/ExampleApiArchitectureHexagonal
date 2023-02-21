using Microsoft.EntityFrameworkCore;
using PersonasMS.Domain.Entities;
using PersonasMS.Domain.Interfaces.Repositories;
using PersonasMS.Infraestructure.Data.DatabaseContext;
using PersonasMS.Infraestructure.Data.UnitOfWork;

namespace PersonasMS.Infraestructure.Data.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonasMsDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Persona> _persona;

        public PersonaRepository(PersonasMsDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _persona = dbContext.Set<Persona>();
            _unitOfWork = unitOfWork;
        }
        public bool InactivarAsync(Persona persona)
        {
            _persona.Attach(persona);
            _dbContext.Entry(persona).State = EntityState.Modified;
            return _unitOfWork.Save() > 0;
        }
    }
}
