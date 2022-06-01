using System.Collections.Generic;
using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using RehearsalSheetMusic = Domain.App.RehearsalSheetMusic;

namespace DAL.App.EF.Mappers
{
    public class ProjectMapper : BaseMapper<DAL.App.DTO.Project, Domain.App.Project>,  IBaseMapper<DAL.App.DTO.Project, Domain.App.Project>
    {
        public ProjectMapper(IMapper mapper) : base(mapper)
        {
        }
   public static Project MapProject(Domain.App.Project domainProject)
        {
            var concerts = new List<Concert>();
            foreach (var concert in domainProject.Concerts!)
            {
                var personConcerts = new HashSet<PersonConcert>();
                foreach (var personConcert in concert.PersonConcerts!)
                {
                    personConcerts.Add(new PersonConcert()
                    {
                        Id = personConcert.Id,
                        Comment = personConcert.Comment,
                        Status = personConcert.Status,
                        AppUserId = personConcert.AppUserId,
                        AppUser = new AppUser()
                        {
                            Id = personConcert.AppUser!.Id,
                            FirstName = personConcert.AppUser!.FirstName,
                            LastName = personConcert.AppUser!.LastName,
                            VoiceGroup = new VoiceGroup()
                            {
                                Id = personConcert.AppUser!.VoiceGroup!.Id,
                                Name = personConcert.AppUser!.VoiceGroup!.Name
                            }
                        }
                    });
                }
                concerts.Add(new Concert()
                {
                    Id = concert.Id,
                    Info = concert.Info,
                    Location = concert.Location,
                    Name = concert.Name,
                    Programme = concert.Programme,
                    Start = concert.Start,
                    PersonConcerts = personConcerts
                });
            }
            
            
            var rehearsals = new List<Rehearsal>();
            foreach (var rehearsal in domainProject.Rehearsals!)
            {
                var personRehearsals = new HashSet<PersonRehearsal>();
                foreach (var personRehearsal in rehearsal.PersonRehearsals!)
                {
                    personRehearsals.Add(new PersonRehearsal()
                    {
                        Id = personRehearsal.Id,
                        Comment = personRehearsal.Comment,
                        Status = personRehearsal.Status,
                        AppUserId = personRehearsal.AppUserId,
                        AppUser = new AppUser()
                        {
                            Id = personRehearsal.AppUser!.Id,
                            FirstName = personRehearsal.AppUser!.FirstName,
                            LastName = personRehearsal.AppUser!.LastName,
                            VoiceGroup = new VoiceGroup()
                            {
                                Id = personRehearsal.AppUser!.VoiceGroup!.Id,
                                Name = personRehearsal.AppUser!.VoiceGroup!.Name
                            }
                        }
                    });
                }
                rehearsals.Add(new Rehearsal()
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


            var personProjects = new List<PersonProject>();

            foreach (var personProject in domainProject.PersonProjects!)
            {
                personProjects.Add(new PersonProject()
                {
                    Id = personProject.Id,
                    Comment = personProject.Comment,
                    Status = personProject.Status,
                    AppUserId = personProject.AppUserId,
                    AppUser = new AppUser()
                    {
                        Id = personProject.AppUser!.Id,
                        FirstName = personProject.AppUser!.FirstName,
                        LastName = personProject.AppUser!.LastName,
                        VoiceGroup = new VoiceGroup()
                        {
                            Id = personProject.AppUser!.VoiceGroup!.Id,
                            Name = personProject.AppUser!.VoiceGroup!.Name
                        }
                    }
                });
            }
            
            var dto = new Project()
            {
                Id = domainProject.Id,
                Name = domainProject.Name,
                Programme = domainProject.Programme,
                Info = domainProject.Info,
                PersonProjects = personProjects,
                Rehearsals = rehearsals,
                Concerts = concerts
            };
            return dto;
        }
    }
}