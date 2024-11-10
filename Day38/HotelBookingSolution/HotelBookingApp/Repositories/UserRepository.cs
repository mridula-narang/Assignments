using HotelBookingApp.Contexts;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Interfaces;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly HotelContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(HotelContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not add user");
                throw new CouldNotAddException("User");
            }
        }

        public async Task<User> Delete(int key)
        {
            var user = await Get(key);
            if (user == null)
            {
                throw new NotFoundException("User");
            }
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not delete user");
                throw new Exception("Unable to delete");
            }
        }

<<<<<<< HEAD
        //modify this method to return a user object

        public async Task<User> Get(int key)
        {

=======
        public async Task<User> Get(int key)
        {
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == key);
                return user;
<<<<<<< HEAD

=======
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not get user");
                throw new NotFoundException("User");
            }
        }

<<<<<<< HEAD

=======
>>>>>>> 76a83b798404e0228ee30b6390690c0b63af6e2e
        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count == 0)
            {
                throw new CollectionEmptyException("Users");
            }
            return users;
        }

        public async Task<User> Update(int key, User entity)
        {
            var user = await Get(key);
            if (user == null)
            {
                throw new NotFoundException("User");
            }
            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not update user details");
                throw new Exception("Unable to modify user object");
            }
        }

    }
}
