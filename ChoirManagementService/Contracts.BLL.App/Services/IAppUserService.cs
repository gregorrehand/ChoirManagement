using System;
using System.Collections.Generic;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using AppUser = Domain.App.Identity.AppUser;

namespace Contracts.BLL.App.Services
{
    public interface IAppUserService : IBaseEntityService<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>
    {
    }
}