using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class RehearsalMapper : BaseMapper<DAL.App.DTO.Rehearsal, Domain.App.Rehearsal>,  IBaseMapper<DAL.App.DTO.Rehearsal, Domain.App.Rehearsal>
    {
        public RehearsalMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}