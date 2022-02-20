using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ConcertSheetMusicMapper : BaseMapper<DAL.App.DTO.ConcertSheetMusic, Domain.App.ConcertSheetMusic>,  IBaseMapper<DAL.App.DTO.ConcertSheetMusic, Domain.App.ConcertSheetMusic>
    {
        public ConcertSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}