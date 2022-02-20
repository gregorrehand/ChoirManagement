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
    public class PersonProjectService: BaseEntityService<IAppUnitOfWork, IPersonProjectRepository, BLLAppDTO.PersonProject, DALAppDTO.PersonProject>, IPersonProjectService
    {
        public PersonProjectService(IAppUnitOfWork serviceUow, IPersonProjectRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PersonProjectMapper(mapper))
        {
        }
    }
}