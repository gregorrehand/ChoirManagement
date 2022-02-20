
using Contracts.DAL.Base.Repositories;

using ConcertSheetMusic = DAL.App.DTO.ConcertSheetMusic;

namespace Contracts.DAL.App.Repositories
{
    public interface IConcertSheetMusicRepository : IBaseRepository<ConcertSheetMusic>, IConcertSheetMusicRepositoryCustom<ConcertSheetMusic>
    {

    }
    public interface IConcertSheetMusicRepositoryCustom<TEntity>
    {
    }
}