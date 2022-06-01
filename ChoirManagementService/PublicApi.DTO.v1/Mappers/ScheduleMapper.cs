using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
 public class ScheduleMapper : BaseMapper<ScheduleDTO, BLL.App.DTO.PersonProject>
    {
        public ScheduleMapper(IMapper mapper) : base(mapper)
        {
        }
        public static ScheduleDTO MapToDTO(BLL.App.DTO.PersonProject bllObject)
        {
            var rehearsals = new List<ScheduleRehearsalDTO>();
            var concerts = new List<ScheduleConcertDTO>();

            foreach (var concert in bllObject.Project!.Concerts!)
            {
                var concertDto = new ConcertDTO()
                {
                    Name = concert.Name,
                    Programme = concert.Programme,
                    Info = concert.Info,
                    Location = concert.Location,
                    Start = concert.Start,
                    ProjectId = concert.ProjectId,
                    Id = concert.Id
                };
                if (concert.PersonConcerts != null)
                {
                    var dto = new ScheduleConcertDTO()
                    {
                        Id = concert.PersonConcerts!.First().Id, //Since this is user based, only on PersonConcert will exist
                        AppUserId = concert.PersonConcerts!.First().AppUserId,
                        Status = concert.PersonConcerts!.First().Status,
                        Comment = concert.PersonConcerts!.First().Comment,
                        ConcertDTO = concertDto
                    };
                    concerts.Add(dto);
                }

            }
            
            foreach (var rehearsal in bllObject.Project!.Rehearsals!)
            {
                var rehearsalDto = new RehearsalDTO()
                {
                    End = rehearsal.End,
                    RehearsalProgramme = rehearsal.RehearsalProgramme,
                    Info = rehearsal.Info,
                    Location = rehearsal.Location,
                    Start = rehearsal.Start,
                    ProjectId = rehearsal.ProjectId,
                    Id = rehearsal.Id
                };
                var dto = new ScheduleRehearsalDTO()
                {
                    Id = rehearsal.PersonRehearsals!.First().Id, //Since this is user based, only on PersonConcert will exist
                    AppUserId = rehearsal.PersonRehearsals!.First().AppUserId,
                    Status = rehearsal.PersonRehearsals!.First().Status,
                    Comment = rehearsal.PersonRehearsals!.First().Comment,
                    RehearsalDTO = rehearsalDto
                };
                rehearsals.Add(dto);
            }
            
            
            var projectDto = new ScheduleProjectDTO
            {
                Name = bllObject.Project!.Name,
                Programme = bllObject.Project!.Programme,
                Info = bllObject.Project!.Info,
                Id = bllObject.Project!.Id,
                Rehearsals = rehearsals,
                Concerts = concerts,
            };
            var Dto = new ScheduleDTO
            {
                Id = bllObject.Id,
                AppUserId = bllObject.AppUserId,
                Status = bllObject.Status,
                Comment = bllObject.Comment,
                Project = projectDto
            };
            return Dto;
        }
    }
}