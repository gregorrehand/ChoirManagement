using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class NotificationMapper: BaseMapper<BLL.App.DTO.Notification, DAL.App.DTO.Notification>, IBaseMapper<BLL.App.DTO.Notification, DAL.App.DTO.Notification>

    {
        public NotificationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}