using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AppUserRepository : BaseRepository<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser, Domain.App.Identity.AppUser, AppDbContext >, IAppUserRepository
    {
        public AppUserRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AppUserMapper(mapper))
        {
        }

        public override async Task<IEnumerable<AppUser>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(u => u.VoiceGroup);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e)!);
            return result; 
        }
    }
}