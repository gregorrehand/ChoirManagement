using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ProjectAddDTO
    {
        [MaxLength(128)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] 
        public string Programme { get; set; } = default!;
        [MaxLength(4096)] 
        public string? Info { get; set; }
    }
    public class ProjectDTO
    {
        public Guid Id { get; set; }

        [MaxLength(128)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] 
        public string Programme { get; set; } = default!;
        [MaxLength(4096)] 
        public string? Info { get; set; }
        
        public ICollection<PersonProjectDTO>? PersonProjects { get; set; }
        public ICollection<ProjectRehearsalDTO>? Rehearsals { get; set; }
        public ICollection<ProjectConcertDTO>? Concerts { get; set; }
        
        //public ICollection<ProjectSheetMusic>? ProjectSheetMusics { get; set; } //TODO: Figure something out
    }

    public class PersonProjectDTO
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; } = default!;
        
        public AppUser? AppUser { get; set; }

        public string? Status { get; set; }
        public string? Comment { get; set; }
        
        public Guid ProjectId { get; set; }

        // public Guid VoiceGroupId { get; set; } = default!; //TODO: separate voicegroup for each project
        // public VoiceGroup? VoiceGroup { get; set; }
        
    }
    
    public class ProjectConcertDTO
    {
        public Guid Id { get; set; }

        [MaxLength(128)] 
        public string? Name { get; set; }  = default!;
        [MaxLength(1024)] 
        public string? Programme { get; set; } = default!;
        [MaxLength(4096)]

        public string Info { get; set; }  = default!;
        [MaxLength(128)] 
        public string Location { get; set; } = default!;

        public DateTime Start { get; set; } = default!;
        
        public ICollection<ProjectPersonConcertDTO>? PersonConcerts { get; set; }

    }
    
    public class ProjectPersonConcertDTO

    {
        public Guid Id { get; set; }

        public string? Status { get; set; }
        public string? Comment { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        
        public AppUser AppUser { get; set; } = default!;
    }
    
    public class ProjectRehearsalDTO
    {
        public Guid Id { get; set; }

        [MaxLength(128)] 
        public string Location { get; set; } = default!;
        [MaxLength(1024)] 
        public string RehearsalProgramme { get; set; } = default!;
        [MaxLength(1024)] 
        public string? Info { get; set; }

        public DateTime Start { get; set; } = default!;

        public DateTime End { get; set; } = default!;

        public ICollection<ProjectPersonRehearsalDTO>? PersonRehearsals{ get; set; }

    }
    public class ProjectPersonRehearsalDTO

    {
        public Guid Id { get; set; }

        public string? Status { get; set; }
        public string? Comment { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        
        public AppUser AppUser { get; set; } = default!;
    }

    public class AppUser
    {
        public Guid Id { get; set; }
        
        public string FirstLastName { get; set; } = default!;
        
        
        public string VoiceGroupName { get; set; } = default!;
    }
}