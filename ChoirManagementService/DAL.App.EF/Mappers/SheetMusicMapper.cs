using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class SheetMusicMapper : BaseMapper<DAL.App.DTO.SheetMusic, Domain.App.SheetMusic>,  IBaseMapper<DAL.App.DTO.SheetMusic, Domain.App.SheetMusic>
    {
        public SheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}