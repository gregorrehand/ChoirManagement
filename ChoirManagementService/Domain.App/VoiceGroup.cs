using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class VoiceGroup : DomainEntityId

    {
        [MaxLength(12)] 
        public string Name { get; set; } = default!;
        
        public ICollection<PersonProject>? PersonProjects { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }


    }
}