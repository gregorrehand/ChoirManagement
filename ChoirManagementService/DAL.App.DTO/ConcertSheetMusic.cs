using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ConcertSheetMusic : DomainEntityId
    {
        public Guid ConcertId { get; set; } = default!;
        public Concert? Concert { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusic? SheetMusic { get; set; }
    }
}