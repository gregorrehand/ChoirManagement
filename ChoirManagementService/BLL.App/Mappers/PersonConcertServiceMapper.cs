using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PersonConcertMapper: BaseMapper<BLL.App.DTO.PersonConcert, DAL.App.DTO.PersonConcert>, IBaseMapper<BLL.App.DTO.PersonConcert, DAL.App.DTO.PersonConcert>

    {
        public PersonConcertMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}