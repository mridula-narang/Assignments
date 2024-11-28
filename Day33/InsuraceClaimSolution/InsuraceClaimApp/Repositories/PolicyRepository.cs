using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Exceptions;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuraceClaimApp.Repositories
{
    public class PolicyRepository : IRepository<int, Policy>
    {
        private readonly InsuranceContext _context;
        private readonly ILogger<PolicyRepository> _logger;

        public PolicyRepository(InsuranceContext context, ILogger<PolicyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Policy> Add(Policy entity)
        {
            try
            {
                _context.Policies.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add policy");
                throw new CouldNotAddException("Policy");
            }
        }

        public async Task<Policy> Delete(int key)
        {
            var policy = await Get(key);
            if (policy == null)
            {
                throw new NotFoundException("Policy");
            }
            try
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
                return policy;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not delete policy");
                throw new Exception("Unable to delete");
            }
        }

        public async Task<Policy> Get(int key)
        {
            try
            {
                var policy = await _context.Policies.FindAsync(key);
                return policy;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not get policy");
                throw new NotFoundException("Policy");
            }
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            var policies = await _context.Policies.ToListAsync();
            if (policies.Count == 0)
            {
                throw new CollectionEmptyException("Policies");
            }
            return policies;
        }

        public async Task<Policy> Update(int key, Policy entity)
        {
            var policy = await Get(key);
            if (policy == null)
            {
                throw new NotFoundException("Policy");
            }
            try
            {
                _context.Policies.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update policy details");
                throw new Exception("Unable to modify policy object");
            }
        }
    }
}
