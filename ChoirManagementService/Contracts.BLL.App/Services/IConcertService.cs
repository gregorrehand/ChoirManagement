using System.Collections.Generic;
using System.Threading.Tasks;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IConcertService : IBaseEntityService<BLLAppDTO.Concert, DALAppDTO.Concert>, IConcertRepositoryCustom<BLLAppDTO.Concert>
    {
        BLLAppDTO.Concert UpdateWithNotification(BLLAppDTO.Concert concert);
    }
}