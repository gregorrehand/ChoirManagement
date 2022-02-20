using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRehearsalRepository : IBaseRepository<PersonRehearsal>, IPersonRehearsalRepositoryCustom<PersonRehearsal>
    {
        
    }
    public interface IPersonRehearsalRepositoryCustom<TEntity>
    {
    }
}