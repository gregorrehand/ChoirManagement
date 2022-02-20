using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Project, Domain.App.Project>().ReverseMap();
            CreateMap<DAL.App.DTO.Concert, Domain.App.Concert>().ReverseMap();
            CreateMap<DAL.App.DTO.Rehearsal, Domain.App.Rehearsal>().ReverseMap();
            CreateMap<DAL.App.DTO.PersonConcert, Domain.App.PersonConcert>().ReverseMap();
            CreateMap<DAL.App.DTO.PersonProject, Domain.App.PersonProject>().ReverseMap();
            CreateMap<DAL.App.DTO.PersonRehearsal, Domain.App.PersonRehearsal>().ReverseMap();
            CreateMap<DAL.App.DTO.News, Domain.App.News>().ReverseMap();
            CreateMap<DAL.App.DTO.Notification, Domain.App.Notification>().ReverseMap();
            CreateMap<DAL.App.DTO.VoiceGroup, Domain.App.VoiceGroup>().ReverseMap();
            CreateMap<DAL.App.DTO.SheetMusic, Domain.App.SheetMusic>().ReverseMap();
            CreateMap<DAL.App.DTO.ConcertSheetMusic, Domain.App.ConcertSheetMusic>().ReverseMap();
            CreateMap<DAL.App.DTO.ProjectSheetMusic, Domain.App.ProjectSheetMusic>().ReverseMap();
            CreateMap<DAL.App.DTO.RehearsalSheetMusic, Domain.App.RehearsalSheetMusic>().ReverseMap();

            
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}

