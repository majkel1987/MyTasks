using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Tasks = new Collection<Task>();
            Categories = new Collection<Category>();
        }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
