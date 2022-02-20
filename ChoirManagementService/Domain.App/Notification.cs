using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Notification : DomainEntityIdUser<AppUser>
    {
        [MaxLength(4096)] 
        public string Body { get; set; } = default!;
        public DateTime TimePosted { get; set; }
        
    }
}