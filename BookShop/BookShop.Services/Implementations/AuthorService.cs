using BookShop.Data;
using BookShop.Data.Models;
using BookShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Implementations
{
    public class AuthorService : AbstractService, IAuthorService
    {
        public AuthorService(BookShopDbContext db) 
            : base(db)
        {
        }

        public bool Create(Author author)
        {
            base.db
                .Authors
                .Add(author);

            base.db.SaveChanges();

            return true;
        }

        public Author Get(int id)
        {
            return base.db
                .Authors
                .Find(id);
        }
    }
}
