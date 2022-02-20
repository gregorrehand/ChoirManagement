using System;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class RehearsalSheetMusic
    {
        public Guid Id { get; set; }
        public Guid RehearsalId { get; set; } = default!;
        public RehearsalDTO? Rehearsal { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusic? SheetMusic { get; set; }
        
    }
}