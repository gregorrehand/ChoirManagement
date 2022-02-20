using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using AppUser = DAL.App.DTO.Identity.AppUser;

namespace DAL.App.DTO
{
    public class Notification : DomainEntityIdUser<AppUser>
    {
        [MaxLength(4096)] 
        public string Body { get; set; } = default!;
        public DateTime TimePosted { get; set; }
    }
}