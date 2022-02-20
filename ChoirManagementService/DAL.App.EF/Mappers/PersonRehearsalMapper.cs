using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PersonRehearsalMapper : BaseMapper<DAL.App.DTO.PersonRehearsal, Domain.App.PersonRehearsal>,  IBaseMapper<DAL.App.DTO.PersonRehearsal, Domain.App.PersonRehearsal>
    {
        public PersonRehearsalMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}