using Microsoft.AspNetCore.Mvc;
using NorthwindMVC.Models;
using System.Diagnostics;

namespace NorthwindMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //insert dbcontext
        private readonly NorthwindDbContext _context;

        public HomeController(ILogger<HomeController> logger, NorthwindDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeList()
        {
            //list employees in asc order by their ids
            var employees = _context.Employees.OrderBy(emp => emp.EmployeeId).ToList();
            return View(employees);
        }
        [HttpGet]
        public IActionResult EmployeeAdd(int id)
        {
            var employee = _context.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]
        public IActionResult EmployeeAdd(Employee entry)
        {
            entry.EmployeeId = _context.Employees.Count() + 1;
            var employee = new Employee()
            {
                EmployeeId = entry.EmployeeId,
                FirstName = entry.FirstName,
                LastName = entry.LastName,
                Title = entry.Title
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public IActionResult EmployeeEdit(int id)
        {
            var employee = _context.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]
        public IActionResult EmployeeEdit(Employee e)
        {
            //e is the new info
            //employee is the existing info
            var employee = _context.Employees.Find(e.EmployeeId);
            if (employee != null)
            {
                employee.FirstName = e.FirstName;
                employee.LastName = e.LastName;
                employee.Title = e.Title;
               _context.SaveChanges();
            }
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public IActionResult EmployeeDelete(int id)
        {
            var employee = _context.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
            return View(employee);
        }
        [HttpPost]
        public IActionResult EmployeeDelete(Employee e)
        {
            var employee = _context.Employees.Find(e.EmployeeId);
            /*
            will get an error here if foreign key restraints are in place
            but if we drop the restraints using this query:
            ALTER TABLE table_name 
            DROP CONSTRAINT foreign_key_name;
            it will work
            */
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("EmployeeList");
        }


        public IActionResult CustomerList()
        {
            var customers = _context.Customers.OrderBy(emp => emp.CustomerId).ToList(); ;
            return View(customers);
        }
        [HttpGet]
        public IActionResult CustomerAdd(string id)
        {
            var customer = _context.Customers.Where(e => e.CustomerId == id).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult CustomerAdd(Customer entry)
        {
            var customer = new Customer()
            {
                CustomerId = entry.CustomerId,
                CompanyName = entry.CompanyName,
                ContactName = entry.ContactName,
                Country = entry.Country
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public IActionResult CustomerEdit(string id)
        {
            var customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult CustomerEdit(Customer c)
        {
            //e is the new info
            //employee is the existing info
            var customer = _context.Customers.Find(c.CustomerId);
            if (customer != null)
            {
                customer.CompanyName = c.CompanyName;
                customer.ContactName = c.ContactName;
                customer.Country = c.Country;
                _context.SaveChanges();
            }

            return RedirectToAction("CustomerList");
        }
        [HttpGet]
        public IActionResult CustomerDelete(string id)
        {
            var customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public IActionResult CustomerDelete(Customer c)
        {
            var customer = _context.Customers.Find(c.CustomerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("CustomerList");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}