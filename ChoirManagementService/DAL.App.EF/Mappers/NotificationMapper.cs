using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class NotificationMapper : BaseMapper<DAL.App.DTO.Notification, Domain.App.Notification>,  IBaseMapper<DAL.App.DTO.Notification, Domain.App.Notification>
    {
        public NotificationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}