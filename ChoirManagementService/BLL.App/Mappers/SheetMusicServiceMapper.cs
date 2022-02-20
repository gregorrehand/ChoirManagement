using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class SheetMusicMapper: BaseMapper<BLL.App.DTO.SheetMusic, DAL.App.DTO.SheetMusic>, IBaseMapper<BLL.App.DTO.SheetMusic, DAL.App.DTO.SheetMusic>

    {
        public SheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}