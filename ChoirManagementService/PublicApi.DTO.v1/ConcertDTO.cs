using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ConcertAddDTO
    {
        [MaxLength(128)] 
        public string? Name { get; set; }  = default!;
        [MaxLength(1024)] 
        public string? Programme { get; set; } = default!;
        [MaxLength(4096)]

        public string? Info { get; set; }  = default!;
        [MaxLength(128)] 
        public string? Location { get; set; } = default!;

        public DateTime Start { get; set; } = default!;
        
        public Guid ProjectId { get; set; } = default!;
    }

    public class ConcertDTO : ConcertAddDTO
    {
        public Guid Id { get; set; }
    }

    public class ConcertSheetMusic
    {
        public Guid ConcertId { get; set; }
        public Guid SheetMusicId { get; set; }
    }
}