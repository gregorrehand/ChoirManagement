using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class NewsMapper : BaseMapper<DAL.App.DTO.News, Domain.App.News>,  IBaseMapper<DAL.App.DTO.News, Domain.App.News>
    {
        public NewsMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}