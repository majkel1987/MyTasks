﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyTasks.Core.Models.Domains
{
    public class Task
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Required(ErrorMessage ="Pole Tytuł jest wymagane!")]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }
        
        [MaxLength(250)]
        [Required(ErrorMessage = "Pole Opis jest wymagane!")]
        [Display(Name = "Opis")]
        public string Description{ get; set; }
        
        [Required(ErrorMessage = "Pole Kategoria jest wymagane!")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Termin")]
        public DateTime? Termin { get; set; }
        
        [Display(Name = "Zrealizowane")]
        public bool IsExecuted { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public Category Category { get; set; }
    }
}
