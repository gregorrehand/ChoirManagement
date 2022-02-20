using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class SheetMusicRepository : BaseRepository<DAL.App.DTO.SheetMusic, Domain.App.SheetMusic, Domain.App.Identity.AppUser, AppDbContext >, ISheetMusicRepository
    {
        public SheetMusicRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new SheetMusicMapper(mapper))
        {
        }

         
    }
}