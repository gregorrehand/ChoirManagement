using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class MyScheduleItem
    {
        public Guid? ProjectId { get; set; }
        public Guid? ConcertOrRehearsalId { get; set; }
        public Guid? PersonConcertOrRehearsalId { get; set; }

        
        public string? Status { get; set; }
        public string? Comment { get; set; }
        
        public string? Type { get; set; }
        
        public string? Programme { get; set; }
        [MaxLength(128)] 
        public string? Location { get; set; }
        public DateTime? Start { get; set; }
        [MaxLength(1024)] 
        public string? Info { get; set; }
        
        
        public string? Name { get; set; }
        public DateTime? End { get; set; }
        
    }
}