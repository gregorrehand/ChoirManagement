using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class RehearsalRepository : BaseRepository<DAL.App.DTO.Rehearsal, Domain.App.Rehearsal, Domain.App.Identity.AppUser, AppDbContext >, IRehearsalRepository
    {
        public RehearsalRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RehearsalMapper(mapper))
        {
        }

         
    }
}