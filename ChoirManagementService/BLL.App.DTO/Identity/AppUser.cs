using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; } = default!;
        
        public Guid? VoiceGroupId { get; set; }
        public VoiceGroup? VoiceGroup { get; set; }
        
        public ICollection<PersonRehearsal>? PersonRehearsals { get; set; }
        public ICollection<PersonProject>? PersonProjects { get; set; }
        public ICollection<PersonConcert>? PersonConcerts { get; set; }
        public ICollection<Notification>? Notifications { get; set; }



        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
    }

}