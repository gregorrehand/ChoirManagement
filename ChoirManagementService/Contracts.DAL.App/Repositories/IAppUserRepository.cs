
using Contracts.DAL.Base.Repositories;

using AppUser = DAL.App.DTO.Identity.AppUser;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>, IAppUserRepositoryCustom<AppUser>
    {

    }
    public interface IAppUserRepositoryCustom<TEntity>
    {
    }
}