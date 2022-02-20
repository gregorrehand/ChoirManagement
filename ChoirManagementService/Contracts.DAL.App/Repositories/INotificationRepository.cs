using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>, INotificationRepositoryCustom<Notification>
    {
        
    }
    public interface INotificationRepositoryCustom<TEntity>
    {
    }
}