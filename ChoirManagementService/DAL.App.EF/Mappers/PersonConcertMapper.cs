using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PersonConcertMapper : BaseMapper<DAL.App.DTO.PersonConcert, Domain.App.PersonConcert>,  IBaseMapper<DAL.App.DTO.PersonConcert, Domain.App.PersonConcert>
    {
        public PersonConcertMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}