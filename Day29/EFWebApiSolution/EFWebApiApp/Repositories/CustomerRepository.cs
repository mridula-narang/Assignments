﻿using EFWebApiApp.Contexts;
using EFWebApiApp.Exceptions;
using EFWebApiApp.Interfaces;
using EFWebApiApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFWebApiApp.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ShoppingContext shoppingContext, ILogger<CustomerRepository> logger)
        {
            _context = shoppingContext;
            _logger = logger;
        }

        public async Task<Customer> Add(Customer entity)
        {
            try
            {
                _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new CouldNotAddException("Customer");
            }
        }

        public async Task<Customer> Delete(int key)
        {
            var customer = await Get(key);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            throw new NotFoundException("Customer for delete");
        }

        public async Task<Customer> Get(int key)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == key);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers.Count == 0)
            {
                throw new CollectionEmptyException("Customers");
            }
            return customers;
        }

        public async Task<Customer> Update(int key,Customer entity)
        {
            var customer = await Get(key);
            if (customer != null)
            {
                customer.Name = entity.Name;
                customer.Email = entity.Email;
                customer.Phone = entity.Phone;
                customer.Age = entity.Age;
                customer.DateOfBirth = entity.DateOfBirth;
                await _context.SaveChangesAsync();
                return customer;
            }
            throw new NotFoundException("Customer for update");
        }
    }
}
