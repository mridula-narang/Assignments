using AutoMapper;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using EFWebApiApp.Models.DTO;
using EFWebApiApp.Repositories;

namespace EFWebApiApp.Services
{
    public class CustomerBasicService : ICustomerBasicService
    {
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IMapper _mapper; // Change type to IMapper

        public CustomerBasicService(IRepository<int, Customer> customerRepository, IMapper mapper) // Add IMapper to constructor
        {
            _customerRepository = customerRepository;
            _mapper = mapper; // Initialize _mapper
        }

        public async Task<int> CreateCustomer(CustomerDTO customer)
        {
            Customer newCustomer = _mapper.Map<Customer>(customer);
            newCustomer.Age = CalculateAgeFromDateTime(customer.DateOfBirth);
            var addedCustomer = await _customerRepository.Add(newCustomer);
            return addedCustomer.Id;
        }

        int CalculateAgeFromDateTime(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
