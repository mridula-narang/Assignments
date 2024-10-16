using EFWebApiApp.Models.DTO;

namespace EFWebApiApp.Interfaces
{
    public interface ICustomerBasicService
    {
        Task<int> CreateCustomer(CustomerDTO customer);
    }
}
