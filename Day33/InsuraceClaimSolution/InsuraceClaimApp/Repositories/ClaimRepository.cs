using InsuraceClaimApp.Contexts;
using InsuraceClaimApp.Exceptions;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuraceClaimApp.Repositories
{
    public class ClaimRepository : IRepository<int, InsuranceClaim>
    {
        private readonly InsuranceContext _context;
        private readonly ILogger<ClaimRepository> _logger;

        public ClaimRepository(InsuranceContext context, ILogger<ClaimRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<InsuranceClaim> Add(InsuranceClaim entity)
        {
            try
            {
                _context.Claims.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add claim");
                throw new CouldNotAddException("Claim");
            }
        }

        public async Task<InsuranceClaim> Delete(int key)
        {
            var claim = await Get(key);
            if (claim == null)
            {
                throw new NotFoundException("Claim");
            }
            try
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                return claim;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not delete claim");
                throw new Exception("Unable to delete");
            }
        }

        public async Task<InsuranceClaim> Get(int key)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(key);
                return claim;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not get claim");
                throw new NotFoundException("Claim");
            }
        }

        public async Task<IEnumerable<InsuranceClaim>> GetAll()
        {
            var claims = await _context.Claims.ToListAsync();
            if (claims.Count == 0)
            {
                throw new CollectionEmptyException("Claims");
            }
            return claims;
        }

        public async Task<InsuranceClaim> Update(int key, InsuranceClaim entity)
        {
            var claim = await Get(key);
            if (claim == null)
            {
                throw new NotFoundException("Claim");
            }
            try
            {
                _context.Claims.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update claim details");
                throw new Exception("Unable to modify claim object");
            }
        }
    }
}
