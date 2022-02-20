using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProjectSheetMusicMapper: BaseMapper<BLL.App.DTO.ProjectSheetMusic, DAL.App.DTO.ProjectSheetMusic>, IBaseMapper<BLL.App.DTO.ProjectSheetMusic, DAL.App.DTO.ProjectSheetMusic>

    {
        public ProjectSheetMusicMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}