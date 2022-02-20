using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonConcertRepository : BaseRepository<DAL.App.DTO.PersonConcert, Domain.App.PersonConcert, Domain.App.Identity.AppUser, AppDbContext >, IPersonConcertRepository
    {
        public PersonConcertRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonConcertMapper(mapper))
        {
        }

        public override async Task<IEnumerable<PersonConcert>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(pc => pc.Concert)
                .ThenInclude(c => c!.ConcertSheetMusics)
                .Include(pc => pc.Concert)
                .ThenInclude(c => c!.Project);

        
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e)!);
            return result; 
        }
        
        public override async Task<PersonConcert?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(pc => pc.Concert)
                .ThenInclude(c => c!.ConcertSheetMusics)
                .Include(pc => pc.Concert)
                .ThenInclude(c => c!.Project);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e!.Id.Equals(id)));
        }
        
    }
}