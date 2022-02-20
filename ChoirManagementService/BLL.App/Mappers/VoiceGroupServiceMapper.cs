using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class VoiceGroupMapper: BaseMapper<BLL.App.DTO.VoiceGroup, DAL.App.DTO.VoiceGroup>, IBaseMapper<BLL.App.DTO.VoiceGroup, DAL.App.DTO.VoiceGroup>

    {
        public VoiceGroupMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}