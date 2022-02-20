using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PersonRehearsalService: BaseEntityService<IAppUnitOfWork, IPersonRehearsalRepository, BLLAppDTO.PersonRehearsal, DALAppDTO.PersonRehearsal>, IPersonRehearsalService
    {
        public PersonRehearsalService(IAppUnitOfWork serviceUow, IPersonRehearsalRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PersonRehearsalMapper(mapper))
        {
        }
        public async Task<IEnumerable<BLLAppDTO.PersonRehearsal>> GetAllByStatus(Guid userId, string status)
        {
            if (status != "pending" || status != "accepted" || status != "declined")
            {
                throw new NotSupportedException("This status is not supported");
            }
            return (await GetAllAsync(userId)).Where(pr => pr.Status == status);
        }
    }
}