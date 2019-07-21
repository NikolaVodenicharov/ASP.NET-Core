using BookShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Interfaces
{
    public interface IAuthorService
    {
        Author Get(int id);

        bool Create(Author author);
    }
}
