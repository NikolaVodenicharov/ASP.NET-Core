using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface IUserService
    {
        List<UserListingServiceModel> AllUsersListing(int page = 1);

        List<string> FindUsersByRole(string roleName);
    }
}
