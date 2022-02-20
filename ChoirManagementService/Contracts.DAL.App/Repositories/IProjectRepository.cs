
using Contracts.DAL.Base.Repositories;

using Project = DAL.App.DTO.Project;

namespace Contracts.DAL.App.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>, IProjectRepositoryCustom<Project>
    {

    }
    public interface IProjectRepositoryCustom<TEntity>
    {
    }
}