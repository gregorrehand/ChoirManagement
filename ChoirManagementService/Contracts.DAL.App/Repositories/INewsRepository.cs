using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INewsRepository : IBaseRepository<News>, INewsRepositoryCustom<News>
    {
        
    }
    public interface INewsRepositoryCustom<TEntity>
    {
    }
}