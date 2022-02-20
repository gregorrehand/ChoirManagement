using System;
using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public IProjectService Projects => 
            GetService<IProjectService>(() => new ProjectService(Uow, Uow.Projects, Mapper));
        public IPersonProjectService PersonProjects =>
            GetService<IPersonProjectService>(() => new PersonProjectService(Uow, Uow.PersonProjects, Mapper));
        public IVoiceGroupService VoiceGroups =>
            GetService<IVoiceGroupService>(() => new VoiceGroupService(Uow, Uow.VoiceGroups, Mapper));

        public IConcertService Concerts =>
            GetService<IConcertService>(() => new ConcertService(Uow, Uow.Concerts, Mapper));
        public INewsService Newses =>
            GetService<INewsService>(() => new NewsService(Uow, Uow.Newses, Mapper));
        public INotificationService Notifications =>
            GetService<INotificationService>(() => new NotificationService(Uow, Uow.Notifications, Mapper));
        public IPersonConcertService PersonConcerts =>
            GetService<IPersonConcertService>(() => new PersonConcertService(Uow, Uow.PersonConcerts, Mapper));
        public IRehearsalService Rehearsals =>
            GetService<IRehearsalService>(() => new RehearsalService(Uow, Uow.Rehearsals, Mapper));
        public IPersonRehearsalService PersonRehearsals =>
            GetService<IPersonRehearsalService>(() => new PersonRehearsalService(Uow, Uow.PersonRehearsals, Mapper));
        public ISheetMusicService SheetMusics =>
            GetService<ISheetMusicService>(() => new SheetMusicService(Uow, Uow.SheetMusics, Mapper));
        public IConcertSheetMusicService ConcertSheetMusics =>
            GetService<IConcertSheetMusicService>(() => new ConcertSheetMusicService(Uow, Uow.ConcertSheetMusics, Mapper));
        public IProjectSheetMusicService ProjectSheetMusics =>
            GetService<IProjectSheetMusicService>(() => new ProjectSheetMusicService(Uow, Uow.ProjectSheetMusics, Mapper));
        public IRehearsalSheetMusicService RehearsalSheetMusics =>
            GetService<IRehearsalSheetMusicService>(() => new RehearsalSheetMusicService(Uow, Uow.RehearsalSheetMusics, Mapper));
        public IAppUserService AppUsers =>
            GetService<IAppUserService>(() => new AppUserService(Uow, Uow.AppUsers, Mapper));
    }

}