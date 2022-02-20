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
    public class ProjectRepository : BaseRepository<DAL.App.DTO.Project, Domain.App.Project, Domain.App.Identity.AppUser, AppDbContext >, IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProjectMapper(mapper))
        {
        }

        public override async Task<IEnumerable<Project>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(p => p.Concerts)
                    .ThenInclude(c => c!.PersonConcerts)
                    .ThenInclude(pc => pc.AppUser)
                    .ThenInclude(au => au!.VoiceGroup)
                .Include(p => p.Rehearsals)
                    .ThenInclude(r => r!.PersonRehearsals)
                    .ThenInclude(pr => pr!.AppUser)
                    .ThenInclude(au => au!.VoiceGroup)
                .Include(p => p.PersonProjects)
                    .ThenInclude(pp => pp!.AppUser)
                    .ThenInclude(au => au!.VoiceGroup);

        
            var domainEntities = await query.ToListAsync();
            var result = new List<Project>();
            foreach (var project in domainEntities)
            {
                result.Add(ProjectMapper.MapProject(project));
            }
            return result; 
        }
    }
}