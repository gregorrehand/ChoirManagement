
using Contracts.DAL.Base.Repositories;

using SheetMusic = DAL.App.DTO.SheetMusic;

namespace Contracts.DAL.App.Repositories
{
    public interface ISheetMusicRepository : IBaseRepository<SheetMusic>, ISheetMusicRepositoryCustom<SheetMusic>
    {

    }
    public interface ISheetMusicRepositoryCustom<TEntity>
    {
    }
}