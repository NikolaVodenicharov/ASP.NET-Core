using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private const int PageSize = 20;
        private LearningSystemDbContext db;
        private IMapper mapper;

        public UserService(LearningSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public List<UserListingServiceModel> AllUsersListing(int page = 1)
        {
            var userListing = this.db
                .Users
                .OrderBy(u => u.UserName)
                .Skip(PageSize * (page - 1))
                .Take(PageSize)
                .ProjectTo<UserListingServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

            return userListing;
        }

        public List<string> FindUsersByRole(string roleName)
        {
            var role = this.db
                .Roles
                .FirstOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                return null;
            }

            var userIds = this.db
                .UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToList();

            return userIds;
        }
    }
}
