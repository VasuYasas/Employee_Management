using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        public static List<Employee> employees = new List<Employee>
            {
                //new Employee
                //{
                //    ID = 1,
                //    FirstName = "Brian",
                //    LastName = "Henry"
                //},

                //new Employee
                //{
                //    ID = 2,
                //    FirstName = "Shawn",
                //    LastName = "Mendes"
                //},

                //new Employee
                //{
                //    ID = 3,
                //    FirstName = "Johon",
                //    LastName = "David"
                //}
        };
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }


        //show the employee list
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
           
            return Ok(await _context.Employees.ToListAsync());

        }

        //search by employee id
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return BadRequest("Employee not found...");
            return Ok(employee);

        }

        //add a new employee to list
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());

        }

        //update employee details in list
        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var dbemployee = await _context.Employees.FindAsync(request.ID);
            if (dbemployee == null)
                return BadRequest("Employee not found...");

            dbemployee.FirstName = request.FirstName;
            dbemployee.LastName = request.LastName;

            await _context.SaveChangesAsync();
            
            return Ok(await _context.Employees.ToListAsync());

        }

        //delete employee form the list
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> Delete(int id)
        {
            var dbemployee = await _context.Employees.FindAsync(id);
            if (dbemployee == null)
                return BadRequest("Employee not found...");

            _context.Employees.Remove(dbemployee);
            await _context.SaveChangesAsync();

            return Ok(employees);

        }
    }
}
