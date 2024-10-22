using EventBookingApp.Models.DTOs;

namespace EventBookingApp.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAll();
        Task<EmployeeDTO> GetById(int key);
        Task<EmployeeDTO> Add(EmployeeDTO entity);
    }
}
