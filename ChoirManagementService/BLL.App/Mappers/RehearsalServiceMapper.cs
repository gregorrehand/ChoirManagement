using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class RehearsalMapper: BaseMapper<BLL.App.DTO.Rehearsal, DAL.App.DTO.Rehearsal>, IBaseMapper<BLL.App.DTO.Rehearsal, DAL.App.DTO.Rehearsal>

    {
        public RehearsalMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}