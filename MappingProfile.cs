using AutoMapper;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.Models;

namespace Online_CV_Builder
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Templates, TemplateModel>();
        }
    }
}
