
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRehearsalRepository : IBaseRepository<Rehearsal>, IRehearsalRepositoryCustom<Rehearsal>
    {

    }
    public interface IRehearsalRepositoryCustom<TEntity>
    {
    }
}