using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Project : DomainEntityId


    {
        [MaxLength(128)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] 
        public string Programme { get; set; } = default!;
        [MaxLength(4096)] 
        public string? Info { get; set; }
        


        
        public ICollection<PersonProject>? PersonProjects { get; set; }
        public ICollection<Rehearsal>? Rehearsals { get; set; }
        public ICollection<Concert>? Concerts { get; set; }
        
        public ICollection<News>? Newses { get; set; }
    }
}