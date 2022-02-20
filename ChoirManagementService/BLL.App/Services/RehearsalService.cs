using System;
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
    public class RehearsalService: BaseEntityService<IAppUnitOfWork, IRehearsalRepository, BLLAppDTO.Rehearsal, DALAppDTO.Rehearsal>, IRehearsalService
    {
        public RehearsalService(IAppUnitOfWork serviceUow, IRehearsalRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new RehearsalMapper(mapper))
        {
        }

        public BLLAppDTO.Rehearsal UpdateWithNotification(BLLAppDTO.Rehearsal rehearsal)
        {
            foreach (var personRehearsal in rehearsal.PersonRehearsals!)
            {
                ServiceUow.Notifications.Add(new DAL.App.DTO.Notification()
                {
                    AppUserId = personRehearsal.AppUserId,
                    Body = "Changes in rehearsal with date: " + rehearsal.Start.Date,
                    TimePosted = DateTime.Now
                });
            }
            return Mapper.Map(ServiceRepository.Update(Mapper.Map(rehearsal)!))!;        
        }
    }
}