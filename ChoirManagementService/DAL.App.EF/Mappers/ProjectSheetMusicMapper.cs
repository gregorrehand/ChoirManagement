using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProjectSheetMusicMapper : BaseMapper<DAL.App.DTO.ProjectSheetMusic, Domain.App.ProjectSheetMusic>,  IBaseMapper<DAL.App.DTO.ProjectSheetMusic, Domain.App.ProjectSheetMusic>
    {
        public ProjectSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}