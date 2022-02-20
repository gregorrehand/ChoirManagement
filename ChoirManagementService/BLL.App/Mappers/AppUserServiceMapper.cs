using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class AppUserMapper: BaseMapper<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>, IBaseMapper<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>

    {
        public AppUserMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}