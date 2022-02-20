using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class VoiceGroupRepository : BaseRepository<DAL.App.DTO.VoiceGroup, Domain.App.VoiceGroup, Domain.App.Identity.AppUser, AppDbContext >, IVoiceGroupRepository
    {
        public VoiceGroupRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new VoiceGroupMapper(mapper))
        {
        }

         
    }
}