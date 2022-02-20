using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
namespace Contracts.BLL.App.Services
{
    public interface IRehearsalService : IBaseEntityService<BLLAppDTO.Rehearsal, DALAppDTO.Rehearsal>, IRehearsalRepositoryCustom<BLLAppDTO.Rehearsal>
    {
        BLLAppDTO.Rehearsal UpdateWithNotification(BLLAppDTO.Rehearsal rehearsal);
    }
}
