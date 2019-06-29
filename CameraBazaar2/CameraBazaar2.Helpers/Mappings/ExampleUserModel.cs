using AutoMapper;
using CameraBazaar2.Data.Models;

namespace CameraBazaar2.Helpers.Mapping
{
    public class ExampleUserModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string MailAddress { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile
                .CreateMap<User, ExampleUserModel>()
                .ForMember(uv => uv.MailAddress, configure => configure.MapFrom(u => u.Email));
        }
    }
}
