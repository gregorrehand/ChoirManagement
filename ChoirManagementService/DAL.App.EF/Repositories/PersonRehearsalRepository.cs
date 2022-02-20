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
    public class PersonRehearsalRepository : BaseRepository<DAL.App.DTO.PersonRehearsal, Domain.App.PersonRehearsal, Domain.App.Identity.AppUser, AppDbContext >, IPersonRehearsalRepository
    {
        public PersonRehearsalRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonRehearsalMapper(mapper))
        {
        }
        public override async Task<IEnumerable<PersonRehearsal>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(pr => pr.Rehearsal)
                .ThenInclude(r => r!.RehearsalSheetMusics)
                .Include(pr => pr.Rehearsal)
                .ThenInclude(r => r!.Project);

        
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e)!);
            return result; 
        }
        public override async Task<PersonRehearsal?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(pr => pr.Rehearsal)
                .ThenInclude(r => r!.RehearsalSheetMusics)
                .Include(pr => pr.Rehearsal)
                .ThenInclude(r => r!.Project);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e!.Id.Equals(id)));
        }
         
    }
}