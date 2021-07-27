using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.API
{
    public class EmployeesController : BaseApiController
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees(){
            return await _context.Employee.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id){
            return await _context.Employee.FindAsync(id);
        }
    }
}