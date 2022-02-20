using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonConcertRepository : IBaseRepository<PersonConcert>, IPersonConcertRepositoryCustom<PersonConcert>
    {
        
    }
    public interface IPersonConcertRepositoryCustom<TEntity>
    {
    }
}