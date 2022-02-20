using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ConcertMapper: BaseMapper<BLL.App.DTO.Concert, DAL.App.DTO.Concert>, IBaseMapper<BLL.App.DTO.Concert, DAL.App.DTO.Concert>

    {
        public ConcertMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}