using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ProjectService: BaseEntityService<IAppUnitOfWork, IProjectRepository, BLLAppDTO.Project, DALAppDTO.Project>, IProjectService
    {
        public ProjectService(IAppUnitOfWork serviceUow, IProjectRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProjectMapper(mapper))
        {
        }
        public async Task<List<BLLAppDTO.Project?>> GetAllSortedByVoiceGroup()
        {
            
            var projects =  (await ServiceRepository.GetAllAsync()).Select(entity => Mapper.Map(entity)).ToList()!;
            foreach (var project in projects)
            {
                foreach (var concert in project!.Concerts!)
                {
                    concert.PersonConcerts = concert.PersonConcerts!.OrderBy(pc => Array.IndexOf(Constants.Constants.VOICEGROUP_ORDER, pc.AppUser!.VoiceGroup!.Name)).ToHashSet();
                }
                foreach (var rehearsal in project!.Rehearsals!)
                {
                    rehearsal.PersonRehearsals = rehearsal.PersonRehearsals!.OrderBy(pc => Array.IndexOf(Constants.Constants.VOICEGROUP_ORDER, pc.AppUser!.VoiceGroup!.Name)).ToHashSet();
                }
                project.PersonProjects = project.PersonProjects!.OrderBy(pc => Array.IndexOf(Constants.Constants.VOICEGROUP_ORDER, pc.AppUser!.VoiceGroup!.Name)).ToList();
            }

            return projects;
        }

        public BLLAppDTO.Project UpdateWithNotification(BLLAppDTO.Project project)
        {
            foreach (var personProject in project.PersonProjects!)
            {
                ServiceUow.Notifications.Add(new DAL.App.DTO.Notification()
                {
                    AppUserId = personProject.AppUserId,
                    Body = "Changes in project with name: " + project.Name,
                    TimePosted = DateTime.Now
                });
            }
            return Mapper.Map(ServiceRepository.Update(Mapper.Map(project)!))!;        
        }
    }
}