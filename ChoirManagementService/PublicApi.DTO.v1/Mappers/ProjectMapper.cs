using System.Collections.Generic;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ProjectMapper : BaseMapper<PublicApi.DTO.v1.ProjectDTO, BLL.App.DTO.Project>
    {
        public ProjectMapper(IMapper mapper) : base(mapper)
        {
        }
        public static ProjectDTO MapToDTO(BLL.App.DTO.Project bllProject)
        {
            var concerts = new List<ProjectConcertDTO>();
            foreach (var concert in bllProject.Concerts!)
            {
                var personConcerts = new List<ProjectPersonConcertDTO>();
                foreach (var personConcert in concert.PersonConcert!)
                {
                    personConcerts.Add(new ProjectPersonConcertDTO()
                    {
                        Id = personConcert.Id,
                        Comment = personConcert.Comment,
                        Status = personConcert.Status,
                        AppUser = new AppUser()
                        {
                            Id = personConcert.AppUser!.Id,
                            FirstLastName = personConcert.AppUser!.FirstLastName,
                            VoiceGroupName = personConcert.AppUser!.VoiceGroup!.Name
                        }
                    });
                }
                concerts.Add(new ProjectConcertDTO()
                {
                    Id = concert.Id,
                    Info = concert.Info!,
                    Location = concert.Location!,
                    Name = concert.Name,
                    Programme = concert.Programme,
                    Start = concert.Start,
                    PersonConcerts = personConcerts
                });
            }
            
            
            var rehearsals = new List<ProjectRehearsalDTO>();
            foreach (var rehearsal in bllProject.Rehearsals!)
            {
                var personRehearsals = new List<ProjectPersonRehearsalDTO>();
                foreach (var personRehearsal in rehearsal.PersonRehearsals!)
                {
                    personRehearsals.Add(new ProjectPersonRehearsalDTO()
                    {
                        Id = personRehearsal.Id,
                        Comment = personRehearsal.Comment,
                        Status = personRehearsal.Status,
                        AppUser = new AppUser()
                        {
                            Id = personRehearsal.AppUser!.Id,
                            FirstLastName = personRehearsal.AppUser!.FirstLastName,
                            VoiceGroupName = personRehearsal.AppUser!.VoiceGroup!.Name
                        }
                    });
                }
                rehearsals.Add(new ProjectRehearsalDTO()
                {
                    Id = rehearsal.Id,
                    Info = rehearsal.Info,
                    Location = rehearsal.Location,
                    RehearsalProgramme = rehearsal.RehearsalProgramme,
                    Start = rehearsal.Start,
                    End = rehearsal.End,
                    PersonRehearsals = personRehearsals
                });
            }


            var personProjects = new List<PersonProjectDTO>();

            foreach (var personProject in bllProject.PersonProjects!)
            {
                personProjects.Add(new PersonProjectDTO()
                {
                    Id = personProject.Id,
                    Comment = personProject.Comment,
                    Status = personProject.Status,
                    AppUser = new AppUser()
                    {
                        Id = personProject.AppUser!.Id,
                        FirstLastName = personProject.AppUser!.FirstLastName,
                        VoiceGroupName = personProject.AppUser!.VoiceGroup!.Name
                    }
                });
            }
   
            var dto = new ProjectDTO()
            {
                Id = bllProject.Id,
                Name = bllProject.Name,
                Programme = bllProject.Programme,
                Info = bllProject.Info,
                PersonProjects = personProjects,
                Rehearsals = rehearsals,
                Concerts = concerts
            };
            return dto;
        }
    }
}
