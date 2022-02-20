using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class RehearsalSheetMusicMapper: BaseMapper<BLL.App.DTO.RehearsalSheetMusic, DAL.App.DTO.RehearsalSheetMusic>, IBaseMapper<BLL.App.DTO.RehearsalSheetMusic, DAL.App.DTO.RehearsalSheetMusic>

    {
        public RehearsalSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}