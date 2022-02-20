using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class PersonProject : DomainEntityIdUser<AppUser>

    {
        [MaxLength(256)]
        public string Status { get; set; } = default!;
        [MaxLength(256)] 
        public string? Comment { get; set; }
        public Guid? VoiceGroupId { get; set; } = default!;
        public VoiceGroup? VoiceGroup { get; set; }
        
        public Guid ProjectId { get; set; } = default!;
        public Project? Project { get; set; }
    }
}