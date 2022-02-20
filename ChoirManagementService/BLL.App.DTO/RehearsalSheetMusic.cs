using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class RehearsalSheetMusic : DomainEntityId
    {
        public Guid RehearsalId { get; set; } = default!;
        public Rehearsal? Rehearsal { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusic? SheetMusic { get; set; }
        
    }
}