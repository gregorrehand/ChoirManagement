using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Contracts.Domain.Base;

namespace DAL.App.DTO
{
    public class VoiceGroup : IDomainEntityId

    {
        [MaxLength(12)] 
        public string Name { get; set; } = default!;
        
        public ICollection<PersonProject>? PersonProjects { get; set; }

        public Guid Id { get; set; }
    }
}