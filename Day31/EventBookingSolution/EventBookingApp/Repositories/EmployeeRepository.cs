using EventBookingApp.Contexts;
using EventBookingApp.Exceptions;
using EventBookingApp.Interfaces;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Repositories
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly EventContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EventContext eventContext, ILogger<EmployeeRepository> logger)
        {
            _context = eventContext;
            _logger = logger;
        }
        public async Task<Employee> Add(Employee entity)
        {
            try
            {
                _context.Employees.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Employee");
            }
        }

        public async Task<Employee> Delete(int key)
        {
            var employee = await Get(key);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            throw new NotFoundException("Employee");
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
           var employees = await _context.Employees.ToListAsync();
           if (employees.Count == 0)
           {
                throw new NotFoundException("Employee");
           }
            return employees;
        }

        public async Task<Employee> Get(int key)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == key);
            if (employee == null)
            {
                throw new NotFoundException("Employee");
            }
            return employee;
        }

        public async Task<Employee> Update(int key,Employee entity)
        {
            var employee = await Get(key);
            if (employee != null)
            {
                employee.Name = entity.Name;
                employee.Department = entity.Department;
                employee.Email = entity.Email;
                employee.Phone = entity.Phone;
                await _context.SaveChangesAsync();
                return employee;
            }
            throw new NotFoundException("Employee");
        }
    }
}
