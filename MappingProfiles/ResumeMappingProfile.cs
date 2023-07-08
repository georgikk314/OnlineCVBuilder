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
                .ReverseMap().PreserveReferences();

            CreateMap<PersonalInfo, PersonalInfoDTO>()
                .ReverseMap().PreserveReferences();

            CreateMap<Education, EducationDTO>().ReverseMap().PreserveReferences();

            CreateMap<WorkExperience, WorkExperienceDTO>().ReverseMap().PreserveReferences();

            CreateMap<Skills, SkillDTO>().ReverseMap().PreserveReferences();

            CreateMap<Languages, LanguageDTO>().ReverseMap().PreserveReferences();

            CreateMap<Locations, LocationDTO>().ReverseMap().PreserveReferences();

            CreateMap<Certificates, CertificateDTO>().ReverseMap().PreserveReferences();

            CreateMap<SkillDTO, ResumeSkills>()
                .ForMember(dto => dto.SkillId, opt => opt.MapFrom(x => x.SkillId)).PreserveReferences();

            CreateMap<LocationDTO, ResumeLocations>()
                .ForMember(dto => dto.LocationId, opt => opt.MapFrom(x => x.LocationId)).PreserveReferences();

            CreateMap<LanguageDTO, ResumeLanguages>()
                .ForMember(dto => dto.LanguageId, opt => opt.MapFrom(x => x.LanguageId)).PreserveReferences();

        }
    }
}
