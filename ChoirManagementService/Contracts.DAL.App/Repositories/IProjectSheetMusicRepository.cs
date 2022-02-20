
using Contracts.DAL.Base.Repositories;

using ProjectSheetMusic = DAL.App.DTO.ProjectSheetMusic;

namespace Contracts.DAL.App.Repositories
{
    public interface IProjectSheetMusicRepository : IBaseRepository<ProjectSheetMusic>, IProjectSheetMusicRepositoryCustom<ProjectSheetMusic>
    {

    }
    public interface IProjectSheetMusicRepositoryCustom<TEntity>
    {
    }
}