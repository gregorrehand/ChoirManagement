using System;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class ConcertSheetMusicDTO
    {
        public Guid Id { get; set; }
        public Guid ConcertId { get; set; } = default!;
        public ConcertDTO? Concert { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusicDTO? SheetMusic { get; set; }
    }
}