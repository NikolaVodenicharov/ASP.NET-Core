﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningSystem.Data;
using LearningSystem.Services.Constants;
using LearningSystem.Services.Interfaces;
using LearningSystem.Services.Models;
using LearningSystem.Services.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class UserService : AbstractService, IUserService
    {
        public UserService(LearningSystemDbContext db, IMapper mapper) 
            : base(db, mapper)
        {
        }

        public List<UserListingServiceModel> AllByPages(int page = PageConstants.DefaultPage)
        {
            return base.db
                .Users
                .OrderBy(u => u.UserName)
                .Skip(PageConstants.PageSize * (page - 1))
                .Take(PageConstants.PageSize)
                .ProjectTo<UserListingServiceModel>(base.mapper.ConfigurationProvider)
                .ToList();
        }

        public List<string> FindIdsByRole(string roleName)
        {
            var role = base.db
                .Roles
                .FirstOrDefault(r => r.Name == roleName);

            if (role == null)
            {
                return null;
            }

            return base.db
                .UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToList();
        }
    }
}