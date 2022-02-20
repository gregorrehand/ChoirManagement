using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProjectService : IBaseEntityService<BLLAppDTO.Project, DALAppDTO.Project>, IProjectRepositoryCustom<BLLAppDTO.Project>
    {
        Task<List<BLLAppDTO.Project?>> GetAllSortedByVoiceGroup();
        BLLAppDTO.Project UpdateWithNotification(BLLAppDTO.Project project);
    }
}