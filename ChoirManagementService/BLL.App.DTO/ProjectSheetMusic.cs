using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ProjectSheetMusic : DomainEntityId
    {
        public Guid ProjectId { get; set; } = default!;
        public Project? Project { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusic? SheetMusic { get; set; }
    }
}