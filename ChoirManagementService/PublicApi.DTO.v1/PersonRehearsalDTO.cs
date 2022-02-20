using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonRehearsalAddDTO
    {
        public string? Status { get; set; }
        public string? Comment { get; set; }

        public Guid RehearsalID { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;

    }
    
    public class PersonRehearsalDTO : PersonRehearsalAddDTO
    {
        public Guid Id { get; set; }

        public PRehearsalDTO Rehearsal { get; set; } = default!;

    }
    public class PRehearsalDTO
    {
        [MaxLength(128)] 
        public string Location { get; set; } = default!;
        public DateTime Start { get; set; } = default!;
        [MaxLength(1024)] 
        public string RehearsalProgramme { get; set; } = default!;
        [MaxLength(1024)] 
        public string? Info { get; set; }

        public DateTime End { get; set; } = default!;

        public string ProjectName { get; set; } = default!;
        
        //The content of sheet music will be retrieved by id with a separate API call
        public ICollection<SheetMusicDTO>? RehearsalSheetMusics { get; set; }
    }

}