using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ConcertSheetMusicService: BaseEntityService<IAppUnitOfWork, IConcertSheetMusicRepository, BLLAppDTO.ConcertSheetMusic, DALAppDTO.ConcertSheetMusic>, IConcertSheetMusicService
    {
        public ConcertSheetMusicService(IAppUnitOfWork serviceUow, IConcertSheetMusicRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ConcertSheetMusicMapper(mapper))
        {
        }
    }
}