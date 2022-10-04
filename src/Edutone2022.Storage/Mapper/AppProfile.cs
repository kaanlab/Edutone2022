using AutoMapper;
using Edutone2022.Common.Models;
using Edutone2022.Storage.Models;

namespace Edutone2022.Storage.Mapper
{
    public sealed class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ArticleDb, ArticleModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Author.DisplayName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Author.Avatar));

            CreateMap<EmployeeContactDb, EmployeeContactModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<FileDb, FileModel>();
            CreateMap<DocumentDb, DocumentModel>();
            CreateMap<AppUserDb, AppUserModel>()
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));
        }
    }
}
