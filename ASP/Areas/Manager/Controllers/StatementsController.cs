using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Repositories;
using DataBase.EntityFramework;
using Domain.Core.Education;

namespace ASP.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class StatementsController : Controller
    {
        IDataManager datamanager;

        public StatementsController()
        {
            datamanager = new EFDataManager( new ApplicationContext() );
        }

        public IActionResult Index()
        {
            var stats = datamanager.Statements.GetAll();

            return View(stats);
        }

        [HttpGet]
        public IActionResult StatementsByDay( DateTime date ) {
            IEnumerable<Statement> dd = datamanager.Statements.GetAll().Where(s => s.Date.Date == date.Date);
            return View(dd);
        }
    }
}