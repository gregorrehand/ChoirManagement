using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class NotificationRepository : BaseRepository<DAL.App.DTO.Notification, Domain.App.Notification, Domain.App.Identity.AppUser, AppDbContext >, INotificationRepository
    {
        public NotificationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new NotificationMapper(mapper))
        {
        }

         
    }
}