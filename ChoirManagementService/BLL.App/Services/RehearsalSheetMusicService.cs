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
    public class RehearsalSheetMusicService: BaseEntityService<IAppUnitOfWork, IRehearsalSheetMusicRepository, BLLAppDTO.RehearsalSheetMusic, DALAppDTO.RehearsalSheetMusic>, IRehearsalSheetMusicService
    {
        public RehearsalSheetMusicService(IAppUnitOfWork serviceUow, IRehearsalSheetMusicRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new RehearsalSheetMusicMapper(mapper))
        {
        }
    }
}