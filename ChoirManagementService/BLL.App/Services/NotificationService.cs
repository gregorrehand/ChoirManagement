using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class NotificationService: BaseEntityService<IAppUnitOfWork, INotificationRepository, BLLAppDTO.Notification, DALAppDTO.Notification>, INotificationService
    {
        public NotificationService(IAppUnitOfWork serviceUow, INotificationRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new NotificationMapper(mapper))
        {
        }
    }
}