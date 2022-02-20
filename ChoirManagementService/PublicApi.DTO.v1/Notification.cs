using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Notification 
    {
        public Guid Id { get; set; }
        [MaxLength(4096)] 
        public string Body { get; set; } = default!;
        public DateTime TimePosted { get; set; } = DateTime.Now;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

    }
}