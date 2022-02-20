using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using DAL.App.DTO.MappingProfiles;
using DAL.App.EF;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.UnitTests
{
    public class TestBaseServices
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;
        private readonly AppBLL _bll;
        
        // ARRANGE - common
        public TestBaseServices(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            
            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            
            var uow = new AppUnitOfWork(_ctx, new Mapper(new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()))));
            _bll = new AppBLL(uow, new Mapper(new MapperConfiguration(c => c.AddProfile(new BLL.App.DTO.MappingProfiles.AutoMapperProfile()))));

        }
        
        [Fact]
        public async void BaseAddTest()
        {
            var entity = new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test"
            };
            var res = _bll!.VoiceGroups.Add(entity); 
            await _bll.SaveChangesAsync();

            Assert.NotNull(res);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(res);
            Assert.Equal(entity.Name, res.Name);
            Assert.NotEqual(Guid.Empty, res.Id);
        }
        
        [Fact]
        public void BaseAddRangeTest()
        {
            var entities = new List<BLL.App.DTO.VoiceGroup>();
            entities.Add(new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test1"
            });
            entities.Add(new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test2"
            });
            _bll!.VoiceGroups.AddRange(entities);
            _bll.SaveChangesAsync();
            var res = _bll!.VoiceGroups.GetAllAsync().Result.ToList();
            Assert.NotNull(res);
            Assert.Equal(2, res.Count());
            Assert.Equal("Test1", res[0].Name);
            Assert.NotEqual(Guid.Empty, res[0].Id);
            Assert.Equal("Test2", res[1].Name);
            Assert.NotEqual(Guid.Empty, res[1].Id);

        }
        
        // public TBllEntity GetUpdatedEntityAfterSaveChanges(TBllEntity entity)
        // {
        //     var dalEntity = _entityCache[entity]!;
        //     var updatedDalEntity = ServiceRepository.GetUpdatedEntityAfterSaveChanges(dalEntity);
        //     var bllEntity = Mapper.Map(updatedDalEntity)!;
        //     return bllEntity;
        // }
        //
        [Fact]
        public void Update()
        {
            var entity = new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test"
            };
            var res = _bll!.VoiceGroups.Add(entity); 
            _bll.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();

            Assert.NotNull(res);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(res);
            Assert.Equal(entity.Name, res.Name);
            Assert.NotEqual(Guid.Empty, res.Id);

            // var updatedEntity = new VoiceGroup()
            // {
            //     Id = res.Id,
            //     Name = "NewName"
            // };

            var getEntity = _bll.VoiceGroups.FirstOrDefaultAsync(res.Id).Result;
            //_ctx.Entry(entity).State = EntityState.Detached;
            getEntity!.Name = "NewName";
            _bll.VoiceGroups.Update(getEntity);
            _bll.SaveChangesAsync();
            var updatedRes = _bll.VoiceGroups.GetAllAsync().Result.ToList();
            Assert.NotNull(updatedRes);
            Assert.Single(updatedRes);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(updatedRes[0]);
            Assert.Equal(res.Id, updatedRes[0].Id);
            Assert.Equal("NewName", updatedRes[0].Name);
            
        }
        [Fact]
        public async void RemoveBaseTest()
        {
            var entity = new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test"
            };
            var res = _bll!.VoiceGroups.Add(entity); 
            await _bll.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();
            Assert.NotNull(res);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(res);
            Assert.Equal(entity.Name, res.Name);
            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.NotEmpty(_bll.VoiceGroups.GetAllAsync().Result);

            
            var getEntity = _bll.VoiceGroups.FirstOrDefaultAsync(res.Id).Result;
            _ctx.ChangeTracker.Clear();
            _bll.VoiceGroups.Remove(getEntity!);
            await _bll.SaveChangesAsync();
            Assert.Empty(_bll.VoiceGroups.GetAllAsync().Result);
        }
        
        [Fact]
        public async void ExistsAsyncBaseTest()
        {
            var entity = new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test"
            };
            Assert.False(await _bll.VoiceGroups.ExistsAsync(new Guid()));

            var res = _bll!.VoiceGroups.Add(entity); 
            await _bll.SaveChangesAsync();
            Assert.NotNull(res);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(res);
            Assert.Equal(entity.Name, res.Name);
            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.NotEmpty(_bll.VoiceGroups.GetAllAsync().Result);

            Assert.True(await _bll.VoiceGroups.ExistsAsync(res.Id));
        }
        
        [Fact]
        public async void RemoveAsyncBaseTest()
        {
            var entity = new BLL.App.DTO.VoiceGroup()
            {
                Name = "Test"
            };
            var res = _bll!.VoiceGroups.Add(entity); 
            await _bll.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();
            Assert.NotNull(res);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(res);
            Assert.Equal(entity.Name, res.Name);
            Assert.NotEqual(Guid.Empty, res.Id);
            Assert.NotEmpty(_bll.VoiceGroups.GetAllAsync().Result);

            var deleted = _bll.VoiceGroups.RemoveAsync(res.Id).Result;
            await _bll.SaveChangesAsync();
            Assert.NotNull(deleted);
            Assert.IsType<BLL.App.DTO.VoiceGroup>(deleted);
            Assert.Equal(entity.Name, deleted.Name);
            Assert.Equal(res.Id, deleted.Id);
            Assert.Empty(_bll.VoiceGroups.GetAllAsync().Result);

        }
        
        [Fact]
        public async void GetAllSortedByVoiceGroup_CustomMethod_Test()
        {
            var project = _bll.Projects.Add(new Project()
            {
                Info = "info",
                Name = "name",
                Programme = "programme",
            });
            await _bll.SaveChangesAsync();
            //var projectId = _bll.Projects.GetAllAsync().Result.First().Id;

            var concert = _bll.Concerts.Add(new Concert()
            {
                Name = "name",
                Info = "info",
                Programme = "programme",
                ProjectId = project.Id
            });
            await _bll.SaveChangesAsync();

            var rehearsal = _bll.Rehearsals.Add(new Rehearsal()
            {
                Location = "loc",
                Info = "info",
                RehearsalProgramme = "programme",
                ProjectId = project.Id
            });
            await _bll.SaveChangesAsync();

            var sopran = _bll.VoiceGroups.Add(new VoiceGroup()
            {
                Name = "Sopran"
            });
            await _bll.SaveChangesAsync();

            var bass = _bll.VoiceGroups.Add(new VoiceGroup()
            {
                Name = "Bass"
            });
            
            var tenor = _bll.VoiceGroups.Add(new VoiceGroup()
            {
                Name = "Tenor"
            });
            await _bll.SaveChangesAsync();

            var sopranUser = _bll.AppUsers.Add(new AppUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = null,
                Email = null,
                EmailConfirmed = false,
                Id = default,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = null,
                NormalizedUserName = null,
                PasswordHash = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = null,
                TwoFactorEnabled = false,
                UserName = null,
                FirstName = "SopranFirstName",
                LastName = "SopranLastName",
                VoiceGroupId = sopran.Id,
                VoiceGroup = null,
                PersonRehearsals = null,
                PersonProjects = null,
                PersonConcerts = null,
                Notifications = null
            });
            var bassUser = _bll.AppUsers.Add(new AppUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = null,
                Email = null,
                EmailConfirmed = false,
                Id = default,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = null,
                NormalizedUserName = null,
                PasswordHash = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = null,
                TwoFactorEnabled = false,
                UserName = null,
                FirstName = "BassFirstName",
                LastName = "BassLastName",
                VoiceGroupId = bass.Id,
                VoiceGroup = null,
                PersonRehearsals = null,
                PersonProjects = null,
                PersonConcerts = null,
                Notifications = null
            });
            var tenorUser = _bll.AppUsers.Add(new AppUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = null,
                Email = null,
                EmailConfirmed = false,
                Id = default,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = null,
                NormalizedUserName = null,
                PasswordHash = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = null,
                TwoFactorEnabled = false,
                UserName = null,
                FirstName = "BassFirstName",
                LastName = "BassLastName",
                VoiceGroupId = tenor.Id,
                VoiceGroup = null,
                PersonRehearsals = null,
                PersonProjects = null,
                PersonConcerts = null,
                Notifications = null
            });
            await _bll.SaveChangesAsync();

            var personConcert1 = _bll.PersonConcerts.Add(new PersonConcert()
            {
                Status = "pending",
                Comment = "",
                AppUserId = bassUser.Id,
                ConcertId = concert.Id
            });
            var personConcert2 = _bll.PersonConcerts.Add(new PersonConcert()
            {
                Status = "pending",
                Comment = "",
                AppUserId = sopranUser.Id,
                ConcertId = concert.Id
            });
            var personConcert3 = _bll.PersonConcerts.Add(new PersonConcert()
            {
                Status = "pending",
                Comment = "",
                AppUserId = tenorUser.Id,
                ConcertId = concert.Id
            });
            
            var personRehearsal1 = _bll.PersonRehearsals.Add(new PersonRehearsal()
            {
                Status = "pending",
                Comment = "",
                AppUserId = bassUser.Id,
                RehearsalId = rehearsal.Id
            });
            var personRehearsal2 = _bll.PersonRehearsals.Add(new PersonRehearsal()
            {
                Status = "pending",
                Comment = "",
                AppUserId = sopranUser.Id,
                RehearsalId = rehearsal.Id
            });
            var personRehearsal3 = _bll.PersonRehearsals.Add(new PersonRehearsal()
            {
                Status = "pending",
                Comment = "",
                AppUserId = tenorUser.Id,
                RehearsalId = rehearsal.Id
            });
            await _bll.SaveChangesAsync();

            var sortedProject = _bll.Projects.GetAllSortedByVoiceGroup().Result;

            Assert.Single(sortedProject);
            Assert.NotNull(sortedProject.First());
            Assert.Equal(bass.Id, sortedProject!.First()!.Concerts!.First().PersonConcert!.ToList()[2].AppUser!.VoiceGroup!.Id);
            Assert.Equal(tenor.Id, sortedProject!.First()!.Concerts!.First().PersonConcert!.ToList()[1].AppUser!.VoiceGroup!.Id);
            Assert.Equal(sopran.Id, sortedProject!.First()!.Concerts!.First().PersonConcert!.ToList()[0].AppUser!.VoiceGroup!.Id);
            
            Assert.Equal(bass.Id, sortedProject!.First()!.Rehearsals!.First().PersonRehearsals!.ToList()[2].AppUser!.VoiceGroup!.Id);
            Assert.Equal(tenor.Id, sortedProject!.First()!.Rehearsals!.First().PersonRehearsals!.ToList()[1].AppUser!.VoiceGroup!.Id);
            Assert.Equal(sopran.Id, sortedProject!.First()!.Rehearsals!.First().PersonRehearsals!.ToList()[0].AppUser!.VoiceGroup!.Id);

        }
        
         [Fact]
        public void UpdateWithNotification_CustomMethod_Test()
        {
            var project = _bll.Projects.Add(new Project()
            {
                Info = "info",
                Name = "name",
                Programme = "programme",
            });
            _bll.SaveChangesAsync();

            var sopran = _bll.VoiceGroups.Add(new VoiceGroup()
            {
                Name = "Sopran"
            });
            _bll.SaveChangesAsync();
            
            var sopranUser = _bll.AppUsers.Add(new AppUser
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = null,
                Email = null,
                EmailConfirmed = false,
                Id = default,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = null,
                NormalizedUserName = null,
                PasswordHash = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = null,
                TwoFactorEnabled = false,
                UserName = null,
                FirstName = "SopranFirstName",
                LastName = "SopranLastName",
                VoiceGroupId = sopran.Id,
                VoiceGroup = null,
                PersonRehearsals = null,
                PersonProjects = null,
                PersonConcerts = null,
                Notifications = null
            }); 
            _bll.SaveChangesAsync();

            var personProject = _bll.PersonProjects.Add(new PersonProject()
            {
                Status = "pending",
                Comment = "",
                AppUserId = sopranUser.Id,
                ProjectId = project.Id
            });

            _bll.SaveChangesAsync();
            _ctx.ChangeTracker.Clear();
            
            var projectFromDb =  _bll.Projects.FirstOrDefaultAsync(project.Id).Result;
            Assert.NotNull(projectFromDb);
            
            
            projectFromDb!.Name = "NewName";
            projectFromDb.Info = "NewInfo";
            projectFromDb.PersonProjects = new List<PersonProject>();
            projectFromDb.PersonProjects.Add(personProject);

           var updateProject = _bll.Projects.UpdateWithNotification(projectFromDb);
            
            Assert.NotNull(updateProject);
            Assert.Equal("NewName", updateProject.Name);
            Assert.Equal("NewInfo", updateProject.Info);
            Assert.Equal("programme", updateProject.Programme);
            Assert.Equal(1, updateProject.PersonProjects!.Count);

            _bll.SaveChangesAsync();

            var updatedRes = _bll.Notifications.GetAllAsync(sopranUser.Id).Result.ToList();
            Assert.NotNull(updatedRes);
            Assert.Single(updatedRes);
            Assert.Equal(sopranUser.Id, updatedRes[0].AppUserId);
            Assert.Equal("Changes in project with name: " + updateProject.Name, updatedRes[0].Body);

        }

        private Project CreateTestProject()
        {
            var project = new Project()
            {
                
            };
            return project;
        }

    }
}