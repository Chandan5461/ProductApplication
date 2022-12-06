using ProductApplication.Models.Domain;

namespace ProductApplication.Repostries
{
    public interface IUserRepository
    {
        User Authenticate(Login user);
        IEnumerable<User> GetAll();
        User Get(int id);

        User Add(User user);

        User Update(int id, User user);

        User Delete(int id);
    }
}
