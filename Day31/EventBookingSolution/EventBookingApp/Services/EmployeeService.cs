using AutoMapper;
using EventBookingApp.Interfaces;
using EventBookingApp.Models;
using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int,Employee> employeeRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeDTO> Add(EmployeeDTO entity)
        {
            var employee = _mapper.Map<Employee>(entity);
            var result = await _employeeRepository.Add(employee);
            return _mapper.Map<EmployeeDTO>(result);
        }

        public async Task<EmployeeDTO> GetById(int key)
        {
            var employee = await _employeeRepository.Get(key);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var employees = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

    }
}
