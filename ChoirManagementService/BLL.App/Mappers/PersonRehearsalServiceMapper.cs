using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PersonRehearsalMapper: BaseMapper<BLL.App.DTO.PersonRehearsal, DAL.App.DTO.PersonRehearsal>, IBaseMapper<BLL.App.DTO.PersonRehearsal, DAL.App.DTO.PersonRehearsal>

    {
        public PersonRehearsalMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}