using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IApplicationDbContext _context;
        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get(string userId)
        {
            var categories = _context.Categories
                .Where(x => x.UserId == userId);

            return categories.OrderBy(x => x.Name).ToList();
        }

        public Category Get(int id, string userId)
        {
            var category = _context.Categories.Single(x => x.Id == id && x.UserId == userId);
            return category;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories.Single(x => x.Id == category.Id);
            categoryToUpdate.Name = category.Name;
        }

        public void Delete(int id, string userId)
        {
            var categoryToDelete = _context.Categories.Single(x => x.Id == id && x.UserId == userId);

            _context.Categories.Remove(categoryToDelete);
        }

    }
}
