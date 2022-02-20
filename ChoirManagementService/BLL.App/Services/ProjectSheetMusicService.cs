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
    public class ProjectSheetMusicService: BaseEntityService<IAppUnitOfWork, IProjectSheetMusicRepository, BLLAppDTO.ProjectSheetMusic, DALAppDTO.ProjectSheetMusic>, IProjectSheetMusicService
    {
        public ProjectSheetMusicService(IAppUnitOfWork serviceUow, IProjectSheetMusicRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProjectSheetMusicMapper(mapper))
        {
        }
    }
}