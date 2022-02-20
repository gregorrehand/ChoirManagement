using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IConcertService Concerts { get; }
        INewsService Newses { get; }
        INotificationService Notifications { get; }
        IPersonConcertService PersonConcerts { get; }
        IPersonProjectService PersonProjects { get; }
        IPersonRehearsalService PersonRehearsals { get; }
        IProjectService Projects { get; }
        IRehearsalService Rehearsals { get; }
        IVoiceGroupService VoiceGroups { get; }
        ISheetMusicService SheetMusics { get; }
        IProjectSheetMusicService ProjectSheetMusics { get; }
        IConcertSheetMusicService ConcertSheetMusics { get; }
        IRehearsalSheetMusicService RehearsalSheetMusics { get; }
        IAppUserService AppUsers { get; }


    }

}