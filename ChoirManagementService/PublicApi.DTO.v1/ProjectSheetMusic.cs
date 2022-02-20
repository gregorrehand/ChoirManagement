using System;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class ProjectSheetMusic
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; } = default!;
        public ProjectDTO? Project { get; set; }
        
        public Guid SheetMusicId { get; set; } = default!;
        public SheetMusicDTO? SheetMusic { get; set; }
    }
}