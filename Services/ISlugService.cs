using BlazorServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface ISlugService
    {
        string URLFriendly(string title);

        bool IsUnique(ApplicationDbContext dbContext, string slug);
        bool IsUniqueCategory(ApplicationDbContext dbContext, string slug);
        
    }
}
