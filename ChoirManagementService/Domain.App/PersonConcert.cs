using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class PersonConcert : DomainEntityIdUser<AppUser>

    {
        
        [MaxLength(256)] 
        public string Status { get; set; } = default!;
        [MaxLength(256)] 
        public string? Comment { get; set; }

        public Guid ConcertId { get; set; } = default!;
        public Concert? Concert { get; set; }
    }
}