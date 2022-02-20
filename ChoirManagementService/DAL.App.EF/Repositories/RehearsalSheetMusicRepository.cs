using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class RehearsalSheetMusicRepository : BaseRepository<DAL.App.DTO.RehearsalSheetMusic, Domain.App.RehearsalSheetMusic, Domain.App.Identity.AppUser, AppDbContext >, IRehearsalSheetMusicRepository
    {
        public RehearsalSheetMusicRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RehearsalSheetMusicMapper(mapper))
        {
        }

         
    }
}