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
    public class VoiceGroupService: BaseEntityService<IAppUnitOfWork, IVoiceGroupRepository, BLLAppDTO.VoiceGroup, DALAppDTO.VoiceGroup>, IVoiceGroupService
    {
        public VoiceGroupService(IAppUnitOfWork serviceUow, IVoiceGroupRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new VoiceGroupMapper(mapper))
        {
        }
    }
}