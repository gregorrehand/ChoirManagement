using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ConcertSheetMusicMapper: BaseMapper<BLL.App.DTO.ConcertSheetMusic, DAL.App.DTO.ConcertSheetMusic>, IBaseMapper<BLL.App.DTO.ConcertSheetMusic, DAL.App.DTO.ConcertSheetMusic>

    {
        public ConcertSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}