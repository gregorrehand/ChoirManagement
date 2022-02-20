using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ProjectSheetMusicRepository : BaseRepository<DAL.App.DTO.ProjectSheetMusic, Domain.App.ProjectSheetMusic, Domain.App.Identity.AppUser, AppDbContext >, IProjectSheetMusicRepository
    {
        public ProjectSheetMusicRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProjectSheetMusicMapper(mapper))
        {
        }

         
    }
}