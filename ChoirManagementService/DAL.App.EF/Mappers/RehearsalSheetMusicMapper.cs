using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class RehearsalSheetMusicMapper : BaseMapper<DAL.App.DTO.RehearsalSheetMusic, Domain.App.RehearsalSheetMusic>,  IBaseMapper<DAL.App.DTO.RehearsalSheetMusic, Domain.App.RehearsalSheetMusic>
    {
        public RehearsalSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}