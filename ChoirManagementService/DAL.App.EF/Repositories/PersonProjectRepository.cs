using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class PersonProjectRepository : BaseRepository<DAL.App.DTO.PersonProject, Domain.App.PersonProject, Domain.App.Identity.AppUser, AppDbContext >, IPersonProjectRepository
    {
        public PersonProjectRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonProjectMapper(mapper))
        {
        }

         
    }
}

        // /// <summary>
        // /// Kombineerib kõik kasutajaga seotud proovid ja kontsertdid MyScheduleView jaoks
        // /// </summary>
        // /// <returns></returns>
        // public virtual async Task<IEnumerable<MyScheduleItem>> GetScheduleItem(Guid appUserId)
        // {
        //     List<MyScheduleItem> myScheduleItems = new List<MyScheduleItem>();
        //     // Kõik projektid, kus kasutaja osaleb (ehk antud kasutaja PersonProjects tabeli kõik projektid)
        //     List<Project> projects = (await RepoDbSet.Where(a => a.AppUserId == appUserId).Select(a => a.Project).ToListAsync())!;
        //     foreach (var project in projects)
        //     {
        //         var rehearsals = RepoDbContext.Rehearsals.Where(a => a.ProjectId == project.Id).ToListAsync().Result;
        //         var concerts = RepoDbContext.Concerts.Where(a => a.ProjectId == project.Id).ToListAsync().Result;
        //         foreach (var concert in concerts)
        //         {
        //             var scheduleItems = RepoDbContext.PersonConcerts
        //                 .Where(a => a.AppUserId == appUserId && a.ConcertId == concert.Id)
        //                 .Select(a => new MyScheduleItem()
        //                 {
        //                     ProjectId = concert.ProjectId,
        //                     ConcertOrRehearsalId = concert.Id,
        //                     PersonConcertOrRehearsalId = a.Id,
        //                     Type = "Concert",
        //                     Status = a.Status,
        //                     Comment = a.Comment,
        //                     Programme = concert.Programme,
        //                     Location = concert.Location,
        //                     Start = concert.Start,
        //                     Info = concert.Info,
        //                     Name = concert.Name,
        //                     End = null,
        //                 });
        //             foreach (var item in scheduleItems)
        //             {
        //                 myScheduleItems.Add(item);
        //             }
        //         }
        //         foreach (var rehearsal in rehearsals)
        //         {
        //             var scheduleItems = RepoDbContext.PersonRehearsals
        //                 .Where(a => a.AppUserId == appUserId && a.RehearsalId == rehearsal.Id)
        //                 .Select(a => new MyScheduleItem()
        //                 {
        //                     ProjectId = rehearsal.ProjectId,
        //                     ConcertOrRehearsalId = rehearsal.Id,
        //                     PersonConcertOrRehearsalId = a.Id,
        //                     Type = "Rehearsal",
        //                     Status = a.Status,
        //                     Comment = a.Comment,
        //                     Programme = rehearsal.RehearsalProgramme,
        //                     Location = rehearsal.Location,
        //                     Start = rehearsal.Start,
        //                     Info = rehearsal.Info,
        //                     Name = null,
        //                     End = rehearsal.End,
        //                 });
        //             foreach (var item in scheduleItems)
        //             {
        //                 myScheduleItems.Add(item);
        //             }
        //         }
        //     }
        //     return myScheduleItems;
        //
        // }
        