
using Contracts.DAL.Base.Repositories;

using RehearsalSheetMusic = DAL.App.DTO.RehearsalSheetMusic;

namespace Contracts.DAL.App.Repositories
{
    public interface IRehearsalSheetMusicRepository : IBaseRepository<RehearsalSheetMusic>, IRehearsalSheetMusicRepositoryCustom<RehearsalSheetMusic>
    {

    }
    public interface IRehearsalSheetMusicRepositoryCustom<TEntity>
    {
    }
}