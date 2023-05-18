using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private IApplicationDbContext _context;
        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Task> Get(string userId, 
            bool isExecuted = false, int categoryId = 0, string title = null)
        {
            var tasks = _context.Tasks.Include(x => x.Category)
                .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

            if (categoryId != 0)
                tasks = tasks.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                tasks = tasks.Where(x => x.Title.Contains(title));
            
            return tasks.OrderBy(x => x.Termin).ToList();
        }

        public Task Get(int id, string userId)
        {
            //Znajdź zadanie o Id przekazanym do zapytania i Id usera
            var task = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);
            return task;
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(Task task)
        {
            // Znajdź zadanie
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id); 

            //Zaktualizuj zadanie
            taskToUpdate.CategoryId = task.CategoryId;
            taskToUpdate.Description = task.Description;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Termin = task.Termin;
            taskToUpdate.Title = task.Title;

        }

        public void Finish(int id, string userId)
        {
            // Znajdź zadanie
            var taskToFinish = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            taskToFinish.IsExecuted = true;
        }

        public void Delete(int id, string userId)
        {
            // Znajdź zadanie
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            _context.Tasks.Remove(taskToDelete);
        }
    }
}
