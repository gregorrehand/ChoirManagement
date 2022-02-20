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
using Domain.App;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class PersonConcertService: BaseEntityService<IAppUnitOfWork, IPersonConcertRepository, BLLAppDTO.PersonConcert, DALAppDTO.PersonConcert>, IPersonConcertService
    {
        public PersonConcertService(IAppUnitOfWork serviceUow, IPersonConcertRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PersonConcertMapper(mapper))
        {
        }
        public async Task<IEnumerable<BLLAppDTO.PersonConcert>> GetAllByStatus(Guid userId, string status)
        {
            if (status != Constants.Constants.STATUS_PENDING || status != Constants.Constants.STATUS_DECLINED || status != Constants.Constants.STATUS_ACCEPTED)
            {
                throw new NotSupportedException("This status is not supported");
            }
            return (await GetAllAsync(userId)).Where(pc => pc.Status == status);
        }
    }
}