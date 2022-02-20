using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ConcertRepository : BaseRepository<DAL.App.DTO.Concert, Domain.App.Concert, Domain.App.Identity.AppUser, AppDbContext >, IConcertRepository
    {
        public ConcertRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ConcertMapper(mapper))
        {
        }

         
    }
}