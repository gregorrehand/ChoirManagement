using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface INotificationService : IBaseEntityService<BLLAppDTO.Notification, DALAppDTO.Notification>, INotificationRepositoryCustom<BLLAppDTO.Notification>
    {
    }
}