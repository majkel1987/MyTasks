using MyTasks.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get(string userId);
        Category Get(int id, string userId);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id, string userId);

    }
}
