using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class News : DomainEntityId
    {
        [MaxLength(4096)]
        public string Body { get; set; } = default!;
        [MaxLength(256)]
        public string Title { get; set; } = default!;
        public DateTime TimePosted { get; set; } = DateTime.Now;
        
        [MaxLength(256)]

        public string ProjectName { get; set; } = default!;    }
}