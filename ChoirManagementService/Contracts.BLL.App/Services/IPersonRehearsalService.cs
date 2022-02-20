using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPersonRehearsalService : IBaseEntityService<BLLAppDTO.PersonRehearsal, DALAppDTO.PersonRehearsal>, IPersonRehearsalRepositoryCustom<BLLAppDTO.PersonRehearsal>
    {
        Task<IEnumerable<BLLAppDTO.PersonRehearsal>> GetAllByStatus(Guid userId, string status);

    }
}