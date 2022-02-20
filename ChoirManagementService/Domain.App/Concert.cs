using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Concert  : DomainEntityId
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
        public Project? Project { get; set; }
        
        public ICollection<PersonConcert>? PersonConcerts { get; set; }
        public ICollection<ConcertSheetMusic>? ConcertSheetMusics { get; set; }

    }
}