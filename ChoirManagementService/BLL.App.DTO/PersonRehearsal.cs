using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class PersonRehearsal : DomainEntityIdUser<AppUser>

    {

        [MaxLength(256)]
        public string? Status { get; set; } = default!;
        [MaxLength(256)] 
        public string? Comment { get; set; }
        public Guid RehearsalId { get; set; } = default!;
        public Rehearsal? Rehearsal { get; set; }
    }
}