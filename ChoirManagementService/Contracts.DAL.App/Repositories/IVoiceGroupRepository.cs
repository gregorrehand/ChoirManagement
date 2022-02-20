
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IVoiceGroupRepository : IBaseRepository<VoiceGroup>, IVoiceGroupRepositoryCustom<VoiceGroup>
    {
    }
    public interface IVoiceGroupRepositoryCustom<TEntity>
    {
    }
}