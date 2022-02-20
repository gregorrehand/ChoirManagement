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
    public class SheetMusicService: BaseEntityService<IAppUnitOfWork, ISheetMusicRepository, BLLAppDTO.SheetMusic, DALAppDTO.SheetMusic>, ISheetMusicService
    {
        public SheetMusicService(IAppUnitOfWork serviceUow, ISheetMusicRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new SheetMusicMapper(mapper))
        {
        }
    }
}