using AutoMapper;
using LearningSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Services.Implementations
{
    public abstract class AbstractService
    {
        protected LearningSystemDbContext db;
        protected IMapper mapper;

        public AbstractService(LearningSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
    }
}
