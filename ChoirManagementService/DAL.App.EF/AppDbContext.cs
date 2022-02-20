using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.Domain;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        //private readonly IUserNameProvider _userNameProvider;
        
        public DbSet<Concert> Concerts { get; set; } = default!;
        public DbSet<News> Newses { get; set; }  = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<PersonConcert> PersonConcerts { get; set; }  = default!;
        public DbSet<PersonProject> PersonProjects { get; set; } = default!;
        public DbSet<PersonRehearsal> PersonRehearsals { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<Rehearsal> Rehearsals { get; set; } = default!;
        public DbSet<VoiceGroup> VoiceGroups { get; set; } = default!;
        public DbSet<SheetMusic> SheetMusics { get; set; } = default!;
        public DbSet<ProjectSheetMusic> ProjectSheetMusics { get; set; } = default!;
        public DbSet<RehearsalSheetMusic> RehearsalSheetMusics { get; set; } = default!;
        public DbSet<ConcertSheetMusic> ConcertSheetMusics { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; } = default!;

        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TODO: should be in base dbcontext
            builder.Entity<Translation>().HasKey(k => new { k.Culture, k.LangStringId});
            
              // disable cascade delete initially for everything
              foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
              {
                  relationship.DeleteBehavior = DeleteBehavior.Restrict;
              }
              builder.Entity<PersonConcert>()
                  .HasOne(m => m.Concert)
                  .WithMany(c => c!.PersonConcerts)
                  .OnDelete(DeleteBehavior.Cascade);
              
              builder.Entity<PersonRehearsal>()
                  .HasOne(m => m.Rehearsal)
                  .WithMany(c => c!.PersonRehearsals)
                  .OnDelete(DeleteBehavior.Cascade);
              
              builder.Entity<PersonProject>()
                  .HasOne(m => m.Project)
                  .WithMany(c => c!.PersonProjects)
                  .OnDelete(DeleteBehavior.Cascade);
                            
              builder.Entity<Concert>()
                  .HasOne(m => m.Project)
                  .WithMany(c => c!.Concerts)
                  .OnDelete(DeleteBehavior.Cascade);
                            
              builder.Entity<Rehearsal>()
                  .HasOne(m => m.Project)
                  .WithMany(c => c!.Rehearsals)
                  .OnDelete(DeleteBehavior.Cascade);

// //
//             /*
//             builder.Entity<Contact>()
//                 .HasIndex(x => new {x.PersonId, x.ContactTypeId})
//                 .IsUnique();
// */

        }
    }
}