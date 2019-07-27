using ShoppingCartExercise.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartExercise.Services.Implementations
{
    public abstract class AbstractService
    {
        protected ShoppingCartExerciseDbContext db;

        public AbstractService(ShoppingCartExerciseDbContext db)
        {
            this.db = db;
        }
    }
}
