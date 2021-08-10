using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Job Name required!")]
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public List<Skill> Skills { get; set; }
        public List<SelectListItem> Employers { get; set; }

        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            this.Employers = new List<SelectListItem>();
            this.Skills = skills;
            foreach(var employer in employers)
            {
                this.Employers.Add
                (
                    new SelectListItem
                    {
                        Value = employer.Id.ToString(),
                        Text = employer.Name
                    }
                ); ;
            }
        }
        public  AddJobViewModel()
        {}
    }
}
