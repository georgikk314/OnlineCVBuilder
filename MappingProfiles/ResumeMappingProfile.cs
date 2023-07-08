using AutoMapper;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs.ResumeRelatedDTOs;

namespace Online_CV_Builder.MappingProfiles
{
    public class ResumeMappingProfile : Profile
    {
        public ResumeMappingProfile()
        {
            CreateMap<Resumes, ResumeDTO>()
                .ReverseMap();

            CreateMap<PersonalInfo, PersonalInfoDTO>()
                .ReverseMap();

            CreateMap<Education, EducationDTO>()
                .ReverseMap();

            CreateMap<WorkExperience, WorkExperienceDTO>()
                .ReverseMap();

            CreateMap<Skills, SkillDTO>()
                .ReverseMap();

            CreateMap<Languages, LanguageDTO>()
                .ReverseMap();

            CreateMap<Locations, LocationDTO>()
                .ReverseMap();

            CreateMap<Certificates, CertificateDTO>()
                .ReverseMap();

            CreateMap<SkillDTO, ResumeSkills>()
                .ForMember(dto => dto.SkillId, opt => opt.MapFrom(x => x.SkillId));

            CreateMap<LocationDTO, ResumeLocations>()
                .ForMember(dto => dto.LocationId, opt => opt.MapFrom(x => x.LocationId));

            CreateMap<LanguageDTO, ResumeLanguages>()
                .ForMember(dto => dto.LanguageId, opt => opt.MapFrom(x => x.LanguageId));

        }
    }
}
