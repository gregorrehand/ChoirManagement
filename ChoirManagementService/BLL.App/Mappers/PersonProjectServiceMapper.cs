using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PersonProjectMapper: BaseMapper<BLL.App.DTO.PersonProject, DAL.App.DTO.PersonProject>, IBaseMapper<BLL.App.DTO.PersonProject, DAL.App.DTO.PersonProject>

    {
        public PersonProjectMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}