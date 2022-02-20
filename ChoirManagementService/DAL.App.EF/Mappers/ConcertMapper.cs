using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ConcertMapper : BaseMapper<DAL.App.DTO.Concert, Domain.App.Concert>,  IBaseMapper<DAL.App.DTO.Concert, Domain.App.Concert>
    {
        public ConcertMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}