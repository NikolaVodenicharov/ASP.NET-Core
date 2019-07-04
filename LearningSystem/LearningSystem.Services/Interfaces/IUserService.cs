using LearningSystem.Services.Constants;
using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Interfaces
{
    public interface IUserService
    {
        List<UserListingServiceModel> AllByPages(int page = PageConstants.DefaultPage);

        List<string> FindIdsByRole(string roleName);
    }
}
