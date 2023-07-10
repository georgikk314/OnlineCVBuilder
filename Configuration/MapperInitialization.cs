using AutoMapper;
using Online_CV_Builder.Data.Entities;
using Online_CV_Builder.DTOs;
using Online_CV_Builder.Models;

namespace Online_CV_Builder.Configuration
{
    public class MapperInitialization : Profile
    {
        public MapperInitialization() 
        {
            CreateMap<Users, RegisterDTO>().ReverseMap();
            CreateMap<Users, LoginDTO>().ReverseMap();
            CreateMap<Users, VerifyDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>().ReverseMap();
        }
    }
}
