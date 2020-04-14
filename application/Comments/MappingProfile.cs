using System.Linq;
using AutoMapper;
using domain;

namespace application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
            .ForMember(d=>d.Username, opt=>opt.MapFrom(s=>s.Author.UserName))
            .ForMember(d=>d.DisplayName, o=>o.MapFrom(s=>s.Author.DisplayName))
            .ForMember(d=>d.Image, o=>o.MapFrom(s=>s.Author.Photos.FirstOrDefault(p=>p.IsMain).Url));
        }
    }
}