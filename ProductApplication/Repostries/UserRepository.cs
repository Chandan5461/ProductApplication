using ProductApplication.Data;
using ProductApplication.Models.Domain;

namespace ProductApplication.Repostries
{
    public class UserRepository : IUserRepository
    {
        private readonly PUserDbContext pUserDbContext;

        public UserRepository(PUserDbContext pUserDbContext)
        {
            this.pUserDbContext = pUserDbContext;
        }

        public User Authenticate(Login user)
        {
            var LogedUser = pUserDbContext.Users
                .FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower() && x.Password == user.Password);


            if (LogedUser == null)
            {
                return null;
            }

            LogedUser.Password = "*************";
            return LogedUser;
        }

        public User Add(User user)
        {
            user.Id = 0;
            pUserDbContext.Users.Add(user);
            pUserDbContext.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            var existingUser = pUserDbContext.Users.Find(id);

            if (existingUser == null)
            {
                return null;
            }

            pUserDbContext.Users.Remove(existingUser);
            pUserDbContext.SaveChanges();
            return existingUser;
        }

        public IEnumerable<User> GetAll()
        {
            return pUserDbContext.Users.ToList();
        }

        public User Get(int id)
        {
            return pUserDbContext.Users
                 .FirstOrDefault(x => x.Id == id);
        }

        public User Update(int id, User user)
        {
            var existingUser = pUserDbContext.Users.Find(id);

            if (existingUser != null)

            {
                existingUser.Name = user.Name;
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Role = user.Role;
                pUserDbContext.SaveChanges();
                return existingUser;
            }

            return null;
        }
    }
}
