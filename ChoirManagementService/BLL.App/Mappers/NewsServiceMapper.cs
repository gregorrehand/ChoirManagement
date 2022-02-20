using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class NewsMapper: BaseMapper<BLL.App.DTO.News, DAL.App.DTO.News>, IBaseMapper<BLL.App.DTO.News, DAL.App.DTO.News>

    {
        public NewsMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}