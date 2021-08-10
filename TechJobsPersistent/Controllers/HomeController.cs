using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext _context;

        public HomeController(JobDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = _context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers= _context.Employers.ToList();
            List<Skill> skills = _context.Skills.ToList();

            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Employer employer = _context.Employers.Find(addJobViewModel.EmployerId);    
                Job job = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = employer,
                };
                for (int i = 0; i < selectedSkills.Count(); i++)
                {
                    JobSkill jobSkill = new JobSkill
                    {
                        JobId = job.Id,
                        SkillId = Int32.Parse(selectedSkills[i])
                    };
                    _context.JobSkills.Add(jobSkill);
                }
                _context.Jobs.Add(job);
                _context.SaveChanges();
                return Redirect("Index");
            }
            addJobViewModel.Employers = new List<SelectListItem>();
            List<Employer> employers = _context.Employers.ToList(); 
            foreach (var employer in employers)
            {
                addJobViewModel.Employers.Add
                (
                    new SelectListItem
                    {
                        Value = employer.Id.ToString(),
                        Text = employer.Name
                    }
                );
            }
            return View("Addjob", addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = _context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = _context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
