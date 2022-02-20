using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;

        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }


        
        public IProjectRepository Projects => GetRepository<IProjectRepository>(() => new ProjectRepository(UowDbContext, Mapper));
        public IConcertRepository Concerts => GetRepository<IConcertRepository>(() => new ConcertRepository(UowDbContext, Mapper));
        public IRehearsalRepository Rehearsals => GetRepository<IRehearsalRepository>(() => new RehearsalRepository(UowDbContext, Mapper));
        public INewsRepository Newses => GetRepository<INewsRepository>(() => new NewsRepository(UowDbContext, Mapper));
        public INotificationRepository Notifications => GetRepository<INotificationRepository>(() => new NotificationRepository(UowDbContext, Mapper));
        public IVoiceGroupRepository VoiceGroups => GetRepository<IVoiceGroupRepository>(() => new VoiceGroupRepository(UowDbContext, Mapper));
        public IPersonProjectRepository PersonProjects => GetRepository<IPersonProjectRepository>(() => new PersonProjectRepository(UowDbContext, Mapper));
        public IPersonConcertRepository PersonConcerts => GetRepository<IPersonConcertRepository>(() => new PersonConcertRepository(UowDbContext, Mapper));
        public IPersonRehearsalRepository PersonRehearsals => GetRepository<IPersonRehearsalRepository>(() => new PersonRehearsalRepository(UowDbContext, Mapper));
        public ISheetMusicRepository SheetMusics => GetRepository<ISheetMusicRepository>(() => new SheetMusicRepository(UowDbContext, Mapper));
        public IConcertSheetMusicRepository ConcertSheetMusics => GetRepository<IConcertSheetMusicRepository>(() => new ConcertSheetMusicRepository(UowDbContext, Mapper));
        public IProjectSheetMusicRepository ProjectSheetMusics => GetRepository<IProjectSheetMusicRepository>(() => new ProjectSheetMusicRepository(UowDbContext, Mapper));
        public IRehearsalSheetMusicRepository RehearsalSheetMusics => GetRepository<IRehearsalSheetMusicRepository>(() => new RehearsalSheetMusicRepository(UowDbContext, Mapper));
        public IAppUserRepository AppUsers => GetRepository<IAppUserRepository>(() => new AppUserRepository(UowDbContext, Mapper));

    }
}