using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Repositories;
using DataBase.EntityFramework;

namespace ASP.Controllers
{
    public class StatementsController : Controller
    {
        IDataManager datamanager;

        public StatementsController() {
            datamanager = new EFDataManager( new ApplicationContext() );
        }

        public IActionResult Index()
        {
            var stats = datamanager.Statements.GetAll();

            return View();
        }
    }
}