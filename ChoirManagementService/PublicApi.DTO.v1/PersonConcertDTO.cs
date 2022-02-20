using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PersonConcertAddDTO
    {
        public string? Status { get; set; }
        public string? Comment { get; set; }

        public Guid ConcertId { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;

    }
    
    public class PersonConcertDTO : PersonConcertAddDTO
    {
        public Guid Id { get; set; }

        public PConcertDTO Concert { get; set; } = default!;

    }
    //Used separately in offers and as part of PersonConcertDTO in My Schedule
    public class PConcertDTO
    {
        [MaxLength(128)] 
        public string? Name { get; set; }  = default!;
        [MaxLength(1024)] 
        public string? Programme { get; set; } = default!;
        [MaxLength(4096)]

        public string Info { get; set; }  = default!;
        [MaxLength(128)] 
        public string Location { get; set; } = default!;

        public DateTime Start { get; set; } = default!;

        public string ProjectName { get; set; } = default!;
        
        //The content of sheet music will be retrieved by id with a separate API call
        public ICollection<SheetMusicDTO>? ConcertSheetMusics { get; set; }
    }

}