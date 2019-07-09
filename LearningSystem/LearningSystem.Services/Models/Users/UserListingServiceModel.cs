using AutoMapper;
using LearningSystem.Data.Models;
using LearningSystem.Helpers.Mappings;

namespace LearningSystem.Services.Models.Users
{
    public class UserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
