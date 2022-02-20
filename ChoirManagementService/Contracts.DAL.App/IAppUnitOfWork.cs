using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IConcertRepository Concerts { get; }
        INewsRepository Newses { get; }
        INotificationRepository Notifications { get; }
        IPersonConcertRepository PersonConcerts { get; }
        IPersonProjectRepository PersonProjects { get; }
        IPersonRehearsalRepository PersonRehearsals { get; }
        IProjectRepository Projects { get; }
        IRehearsalRepository Rehearsals { get; }
        IVoiceGroupRepository VoiceGroups { get; }
        ISheetMusicRepository SheetMusics { get; }

        IProjectSheetMusicRepository ProjectSheetMusics { get; }
        IConcertSheetMusicRepository ConcertSheetMusics { get; }
        IRehearsalSheetMusicRepository RehearsalSheetMusics { get; }
        
        IAppUserRepository AppUsers { get; }


    }
}