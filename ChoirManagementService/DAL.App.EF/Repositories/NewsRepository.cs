using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class NewsRepository : BaseRepository<DAL.App.DTO.News, Domain.App.News, Domain.App.Identity.AppUser, AppDbContext >, INewsRepository
    {
        public NewsRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new NewsMapper(mapper))
        {
        }

         
    }
}