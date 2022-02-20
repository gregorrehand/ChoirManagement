using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPersonConcertService : IBaseEntityService<BLLAppDTO.PersonConcert, DALAppDTO.PersonConcert>,
        IPersonConcertRepositoryCustom<BLLAppDTO.PersonConcert>
    {
        Task<IEnumerable<BLLAppDTO.PersonConcert>> GetAllByStatus(Guid userId, string status);
    }
}