using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataBase.EntityFramework;
using ASP.ViewModels;

namespace ASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var ttt = new EFDataManager( new ApplicationContext() );

            var tr = ttt.EducationPrograms.GetAll()
                                            .Where(p => p.ProgramType.Contains("Курс"))
                                            .Where(pr => pr.Active)
                                            .Where(c => c.CategoryId == 1)
                .IncludeMultiple(s => s.EducationalPlanList);


           var r = ttt.EducationPrograms.GetAllIncludeRef(
                        s => s.Category,
                        s => s.EducationType);

            var tt = ttt.Statements.GetAllIncludeRef( fdf => fdf.Certification );

            return View(tr);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            var ttt = new EFDataManager(new ApplicationContext());
            //var tt = ttt.Statements.GetAll()
            //    .IncludeMultiple( s => s.Certification )
            //    .IncludeMultiple( s => s.EducationProgram )
            //    .IncludeMultiple( s => s.Subject );

            //var vm = ttt.EducationPrograms.GetAll().Where(p => p.ProgramType.Contains("Курс"))
            //                                .Where(pr => pr.Active)
            //                                .Where(c => c.CategoryId == 1)
            //                                .IncludeMultiple( p => p.EducationalPlanList )
            //    .Select( i => new PageContactViewModel { Program = i.Title, Subjects = i.EducationalPlanList.Select( l => l.SubjectId.ToString()) } ).ToList();

            var ititi = ttt.EducationalPlans.GetAll()
                            .IncludeMultiple(pl => pl.EducationProgram)
                            .IncludeMultiple(pl => pl.Subject)
                            .Select( item => new PageContactViewModel {
                                Program = new ProgramViewModel { Title = item.EducationProgram.Title, Guid = item.EducationProgram.Guid },
                                Subjects = new SubjectViewModel { Title = item.Subject.Title, Guid = item.Subject.Guid }
                            });

            return View(ititi);
        }

        [HttpPost]
        public IActionResult Contact(ProgramViewModel program, SubjectViewModel subject)
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
