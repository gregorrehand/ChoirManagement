using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ConcertSheetMusicRepository : BaseRepository<DAL.App.DTO.ConcertSheetMusic, Domain.App.ConcertSheetMusic, Domain.App.Identity.AppUser, AppDbContext >, IConcertSheetMusicRepository
    {
        public ConcertSheetMusicRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ConcertSheetMusicMapper(mapper))
        {
        }

         
    }
}