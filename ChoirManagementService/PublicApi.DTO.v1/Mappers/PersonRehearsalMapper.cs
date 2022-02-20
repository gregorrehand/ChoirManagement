using System.Collections.Generic;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PersonRehearsalMapper : BaseMapper<PublicApi.DTO.v1.PersonRehearsalDTO, BLL.App.DTO.PersonRehearsal>
    {
        public PersonRehearsalMapper(IMapper mapper) : base(mapper)
        {
        }
        public static PersonRehearsalDTO MapToDTO(BLL.App.DTO.PersonRehearsal bllPersonRehearsal)
        {
            var sheetMusicDto = new List<SheetMusicDTO>();

            foreach (var rehearsalSheetMusic in bllPersonRehearsal.Rehearsal!.RehearsalSheetMusics!)
            {
                sheetMusicDto.Add(new SheetMusicDTO()
                {
                    Id = rehearsalSheetMusic.SheetMusicId,
                    Name = rehearsalSheetMusic.SheetMusic!.Name
                });
            }

            var rehearsalDto = new PRehearsalDTO()
            {
                Info = bllPersonRehearsal.Rehearsal!.Info,
                RehearsalProgramme = bllPersonRehearsal.Rehearsal!.RehearsalProgramme,
                Location = bllPersonRehearsal.Rehearsal!.Location,
                Start = bllPersonRehearsal.Rehearsal!.Start,
                End = bllPersonRehearsal.Rehearsal!.End,
                ProjectName = bllPersonRehearsal.Rehearsal!.Project!.Name,
                RehearsalSheetMusics = sheetMusicDto
            };
            var dto = new PersonRehearsalDTO()
            {
                Id = bllPersonRehearsal.Id,
                Comment = bllPersonRehearsal.Comment,
                RehearsalID = bllPersonRehearsal.RehearsalId,
                Status = bllPersonRehearsal.Status,
                Rehearsal = rehearsalDto,
                AppUserId = bllPersonRehearsal.AppUserId
            };
            return dto;
        }
    }
}
