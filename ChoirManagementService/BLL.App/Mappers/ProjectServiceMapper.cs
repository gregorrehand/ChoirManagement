using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProjectMapper: BaseMapper<BLL.App.DTO.Project, DAL.App.DTO.Project>, IBaseMapper<BLL.App.DTO.Project, DAL.App.DTO.Project>

    {
        public ProjectMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}