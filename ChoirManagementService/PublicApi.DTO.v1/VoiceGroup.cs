using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class VoiceGroup

    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        
        public ICollection<PersonProjectDTO>? PersonProjects { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
        
    }
}