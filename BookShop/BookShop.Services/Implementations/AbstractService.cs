using BookShop.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Implementations
{
    public abstract class AbstractService
    {
        protected BookShopDbContext db;

        public AbstractService(BookShopDbContext db)
        {
            this.db = db;
        }
    }
}
