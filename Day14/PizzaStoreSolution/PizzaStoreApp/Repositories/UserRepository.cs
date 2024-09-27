using PizzaStoreApp.Exceptions;
using PizzaStoreApp.Interfaces;
using PizzaStoreApp.Models;

namespace PizzaStoreApp.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        Dictionary<string,User> _users = new Dictionary<string, User>
        {
            {"harry", new User { Username = "harry", Password = "1234" } },
            {"ron", new User { Username = "ron", Password = "1234" } },
            {"hermione", new User { Username = "hermione", Password = "1234" } },
        };
        public User Add(User item)
        {
            if (!_users.ContainsKey(item.Username))
            {
                _users.Add(item.Username, item);
            }
            throw new DuplicateUsernameException();
        }

        public User Delete(string key)
        {
            var user = Get(key);
            if (user != null)
            {
                _users.Remove(key);
            }
            return user;
        }

        public User Get(string key)
        {
            var user = _users[key];
            if (user == null)
                throw new UserNotFoundException();
            return user;
        }

        public List<User> GetAll()
        {
            if (_users.Count == 0)
                throw new NoUsersException();
            return _users.Values.ToList();
        }

        public User Update(User Item)
        {
            var user = Get(Item.Username);
            if (user!=null)
            {
                _users[Item.Username] = Item;
            }
            return user;
        }
    }
}
