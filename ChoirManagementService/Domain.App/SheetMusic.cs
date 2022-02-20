using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class SheetMusic: DomainEntityId
    {
        [MaxLength(128)] 
        public string Name { get; set; } = default!;
        public byte[] Content { get; set; } = default!;
        
        public ICollection<ProjectSheetMusic>? ProjectSheetMusics { get; set; }
        public ICollection<ConcertSheetMusic>? ConcertSheetMusics { get; set; }
        public ICollection<RehearsalSheetMusic>? RehearsalSheetMusics { get; set; }

    }
}