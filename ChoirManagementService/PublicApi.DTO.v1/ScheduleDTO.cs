using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ScheduleDTO: PersonBaseDTO
    {
        public ScheduleProjectDTO Project { get; set; } = default!;
    }

    public class ScheduleProjectDTO: ProjectAddDTO
    {
        public Guid Id { get; set; }

        public ICollection<ScheduleRehearsalDTO>? Rehearsals { get; set; }
        public ICollection<ScheduleConcertDTO>? Concerts { get; set; }
        
        //public ICollection<ProjectSheetMusic>? ProjectSheetMusics { get; set; } //TODO: Figure something out
    }

    public class ScheduleRehearsalDTO : PersonBaseDTO
    {
        public RehearsalDTO RehearsalDTO { get; set; } = default!;
    }
    
    public class ScheduleConcertDTO : PersonBaseDTO
    {
        public ConcertDTO ConcertDTO { get; set; } = default!;
    }
}