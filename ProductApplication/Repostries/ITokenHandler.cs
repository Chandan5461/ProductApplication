using ProductApplication.Models.Domain;

namespace ProductApplication.Repostries
{
    public interface ITokenHandler
    {
        String CreateToken(User user);
    }
}
