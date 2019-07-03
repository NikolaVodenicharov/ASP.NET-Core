using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;

namespace LearningSystem.Services.Models
{
    public class UserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
        }
    }
}
