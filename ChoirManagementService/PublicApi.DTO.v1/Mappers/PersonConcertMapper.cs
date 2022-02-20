using System.Collections.Generic;
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PersonConcertMapper : BaseMapper<PublicApi.DTO.v1.PersonConcertDTO, BLL.App.DTO.PersonConcert>
    {
        public PersonConcertMapper(IMapper mapper) : base(mapper)
        {
        }
        public static PersonConcertDTO MapToDTO(BLL.App.DTO.PersonConcert bllPersonConcert)
        {
            var sheetMusicDto = new List<SheetMusicDTO>();

            foreach (var concertSheetMusic in bllPersonConcert.Concert!.ConcertSheetMusics!)
            {
                sheetMusicDto.Add(new SheetMusicDTO()
                {
                    Id = concertSheetMusic.SheetMusicId,
                    Name = concertSheetMusic.SheetMusic!.Name
                });
            }

            var concertDto = new PConcertDTO()
            {
                Name = bllPersonConcert.Concert!.Name,
                Info = bllPersonConcert.Concert!.Info!,
                Programme = bllPersonConcert.Concert!.Programme,
                Location = bllPersonConcert.Concert!.Location!,
                Start = bllPersonConcert.Concert!.Start,
                ProjectName = bllPersonConcert.Concert!.Project!.Name,
                ConcertSheetMusics = sheetMusicDto
            };
            var dto = new PersonConcertDTO()
            {
                Id = bllPersonConcert.Id,
                Comment = bllPersonConcert.Comment,
                ConcertId = bllPersonConcert.ConcertId,
                Status = bllPersonConcert.Status,
                Concert = concertDto,
                AppUserId = bllPersonConcert.AppUserId
            };
            return dto;
        }
    }
}
