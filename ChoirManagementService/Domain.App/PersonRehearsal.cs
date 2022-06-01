using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class PersonRehearsal  : DomainEntityIdUser<AppUser>
    {

        [MaxLength(256)]
        public string Status { get; set; } = default!;
        [MaxLength(256)] 
        public string? Comment { get; set; }

        public Guid RehearsalId { get; set; } = default!;
        public Rehearsal? Rehearsal { get; set; }
    }
}