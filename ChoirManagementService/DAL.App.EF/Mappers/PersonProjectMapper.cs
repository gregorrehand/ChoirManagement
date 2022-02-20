using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class PersonProjectMapper : BaseMapper<DAL.App.DTO.PersonProject, Domain.App.PersonProject>,  IBaseMapper<DAL.App.DTO.PersonProject, Domain.App.PersonProject>
    {
        public PersonProjectMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}