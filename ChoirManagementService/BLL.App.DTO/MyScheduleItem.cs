using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class MyScheduleItem
    {
        public Guid ProjectId { get; set; }
        public Guid? ConcertOrRehearsalId { get; set; }

        public Guid? PersonConcertOrRehearsalId { get; set; }

        
        public string Status { get; set; } = default!;
        public string Comment { get; set; } = default!;
        
        public string Type { get; set; } = default!;
        
        public string Programme { get; set; } = default!;
        [MaxLength(128)] 
        public string Location { get; set; } = default!;
        public DateTime Start { get; set; } = default!;
        [MaxLength(1024)] 
        public string? Info { get; set; }
        
        
        public string? Name { get; set; }
        public DateTime? End { get; set; }
        
    }
}