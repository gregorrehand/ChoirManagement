using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class VoiceGroupMapper : BaseMapper<DAL.App.DTO.VoiceGroup, Domain.App.VoiceGroup>,  IBaseMapper<DAL.App.DTO.VoiceGroup, Domain.App.VoiceGroup>
    {
        public VoiceGroupMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}