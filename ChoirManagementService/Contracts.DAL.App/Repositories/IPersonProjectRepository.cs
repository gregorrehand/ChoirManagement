
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonProjectRepository : IBaseRepository<PersonProject>, IPersonProjectRepositoryCustom<PersonProject>
    {

    }
    public interface IPersonProjectRepositoryCustom<TEntity>
    {
    }
}