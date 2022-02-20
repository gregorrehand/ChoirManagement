using AutoMapper;

namespace PublicApi.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDTO, BLL.App.DTO.Project>().ReverseMap();
            CreateMap<ConcertDTO, BLL.App.DTO.Concert>().ReverseMap();
            CreateMap<RehearsalDTO, BLL.App.DTO.Rehearsal>().ReverseMap();
            CreateMap<PersonConcertDTO, BLL.App.DTO.PersonConcert>().ReverseMap();
            CreateMap<PersonProjectDTO, BLL.App.DTO.PersonProject>().ReverseMap();
            CreateMap<PersonRehearsalDTO, BLL.App.DTO.PersonRehearsal>().ReverseMap();
            CreateMap<News, BLL.App.DTO.News>().ReverseMap();
            CreateMap<Notification, BLL.App.DTO.Notification>().ReverseMap();
            CreateMap<VoiceGroup, BLL.App.DTO.VoiceGroup>().ReverseMap();
            CreateMap<SheetMusic, BLL.App.DTO.SheetMusic>().ReverseMap();
            CreateMap<ConcertSheetMusic, BLL.App.DTO.ConcertSheetMusic>().ReverseMap();
            CreateMap<ProjectSheetMusic, BLL.App.DTO.ProjectSheetMusic>().ReverseMap();
            CreateMap<RehearsalSheetMusic, BLL.App.DTO.RehearsalSheetMusic>().ReverseMap();
            
            //Creation mapping
            CreateMap<AddNews, BLL.App.DTO.News>();
            CreateMap<ConcertAddDTO, BLL.App.DTO.Concert>();
            CreateMap<RehearsalAddDTO, BLL.App.DTO.Rehearsal>();
            CreateMap<ProjectAddDTO, BLL.App.DTO.Project>();

            
            CreateMap<AppUser, BLL.App.DTO.Identity.AppUser>().ReverseMap();
        }
    }
}

