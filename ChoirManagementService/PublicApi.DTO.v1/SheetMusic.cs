using System;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class SheetMusic
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public byte[] Content { get; set; } = default!;

    }
}