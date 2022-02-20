using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Rehearsal : DomainEntityId

    {

        [MaxLength(128)] 
        public string Location { get; set; } = default!;
        public DateTime Start { get; set; } = default!;
        [MaxLength(1024)] 
        public string RehearsalProgramme { get; set; } = default!;
        [MaxLength(1024)] 
        public string? Info { get; set; }

        public DateTime End { get; set; } = default!;

        public Guid ProjectId { get; set; } = default!;
        public Project? Project { get; set; }
        
        public ICollection<PersonRehearsal>? PersonRehearsals { get; set; }
        public ICollection<RehearsalSheetMusic>? RehearsalSheetMusics { get; set; }

    }
}