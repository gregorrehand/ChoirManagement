using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    
    public class AddNews
    {
        [MaxLength(4096)]
        public string Body { get; set; } = default!;
        
        [MaxLength(256)]
        public string Title { get; set; } = default!;
        
        [MaxLength(256)]

        public string ProjectName { get; set; } = default!;
    }
    
    public class News
    {
        public Guid Id { get; set; }
        [MaxLength(4096)]
        public string Body { get; set; } = default!;
        
        [MaxLength(256)]
        public string Title { get; set; } = default!;
        public DateTime TimePosted { get; set; } = DateTime.Now;

        [MaxLength(256)]

        public string ProjectName { get; set; } = default!;

    }
}