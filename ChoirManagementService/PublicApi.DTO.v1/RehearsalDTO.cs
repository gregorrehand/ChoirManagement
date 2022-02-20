using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class RehearsalAddDTO
    {
        [MaxLength(1024)] 
        public string? RehearsalProgramme { get; set; } = default!;
        [MaxLength(4096)]

        public string? Info { get; set; }  = default!;
        [MaxLength(128)] 
        public string? Location { get; set; } = default!;

        public DateTime Start { get; set; } = default!;
        public DateTime End { get; set; } = default!;
        
        public Guid ProjectId { get; set; } = default!;
    }

    public class RehearsalDTO : RehearsalAddDTO
    {
        public Guid Id { get; set; }
    }
}