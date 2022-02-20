using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface INewsService : IBaseEntityService<BLLAppDTO.News, DALAppDTO.News>, INewsRepositoryCustom<BLLAppDTO.News>
    {
        
    }
}