using AutoMapper;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Project, DAL.App.DTO.Project>().ReverseMap();
            CreateMap<BLL.App.DTO.Concert, DAL.App.DTO.Concert>().ReverseMap();
            CreateMap<BLL.App.DTO.Rehearsal, DAL.App.DTO.Rehearsal>().ReverseMap();
            CreateMap<BLL.App.DTO.PersonConcert, DAL.App.DTO.PersonConcert>().ReverseMap();
            CreateMap<BLL.App.DTO.PersonProject, DAL.App.DTO.PersonProject>().ReverseMap();
            CreateMap<BLL.App.DTO.PersonRehearsal, DAL.App.DTO.PersonRehearsal>().ReverseMap();
            CreateMap<BLL.App.DTO.News, DAL.App.DTO.News>().ReverseMap();
            CreateMap<BLL.App.DTO.Notification, DAL.App.DTO.Notification>().ReverseMap();
            CreateMap<BLL.App.DTO.VoiceGroup, DAL.App.DTO.VoiceGroup>().ReverseMap();
            CreateMap<BLL.App.DTO.SheetMusic, DAL.App.DTO.SheetMusic>().ReverseMap();
            CreateMap<BLL.App.DTO.ConcertSheetMusic, DAL.App.DTO.ConcertSheetMusic>().ReverseMap();
            CreateMap<BLL.App.DTO.ProjectSheetMusic, DAL.App.DTO.ProjectSheetMusic>().ReverseMap();
            CreateMap<BLL.App.DTO.RehearsalSheetMusic, DAL.App.DTO.RehearsalSheetMusic>().ReverseMap();

            
            CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }
}

