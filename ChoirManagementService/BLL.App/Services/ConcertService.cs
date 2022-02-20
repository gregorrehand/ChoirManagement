using System;
using System.Threading.Tasks;
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
    public class ConcertService: BaseEntityService<IAppUnitOfWork, IConcertRepository, BLLAppDTO.Concert, DALAppDTO.Concert>, IConcertService
    {
        public ConcertService(IAppUnitOfWork serviceUow, IConcertRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ConcertMapper(mapper))
        {
        }

        public BLLAppDTO.Concert UpdateWithNotification(BLLAppDTO.Concert concert)
        {
            foreach (var personConcert in concert.PersonConcert!)
            {
                ServiceUow.Notifications.Add(new DAL.App.DTO.Notification()
                {
                    AppUserId = personConcert.AppUserId,
                    Body = "Changes in concert with name: " + concert.Name + " date: " + concert.Start.Date,
                    TimePosted = DateTime.Now
                });
            }
            return Mapper.Map(ServiceRepository.Update(Mapper.Map(concert)!))!;
        }
    }
}